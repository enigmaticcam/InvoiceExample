using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace Invoice_Logic.Caching.InMemory;

public class CacheInMemory : ICache
{
    private IMemoryCache _cache;
    private Action<ICacheEntry> _configOptions;
    private CacheOptions _cacheOptions;
    private List<Func<Task>> _queue = new();
    private SemaphoreSlim _semaphore = new(1, 1);

    public CacheInMemory(CacheOptions options, IMemoryCache cache)
    {
        _cacheOptions = options;
        _cache = cache;
        _configOptions = x =>
        {
            x.SetAbsoluteExpiration(TimeSpan.FromSeconds(options.AbsoluteExpirationInSeconds));
            x.SetSlidingExpiration(TimeSpan.FromSeconds(options.SlidingExpirationInSeconds));
        };
    }

    public async Task CommitQueue()
    {
        foreach (var t in _queue)
        {
            await t();
        }
        _queue.Clear();
    }

    public Task<List<T>> Get<T>(string key, IEnumerable<string> fields)
    {
        var dictionary = GetDictionary<T>(key);
        return Task.FromResult(fields
            .Where(dictionary.ContainsKey)
            .Select(x => dictionary[x])
            .ToList());
    }

    private ConcurrentDictionary<string, T> GetDictionary<T>(string key)
    {
        var result = _cache.GetOrCreate(key, x =>
        {
            _configOptions(x);
            return new ConcurrentDictionary<string, T>();
        });
        if (result == null)
        {
            throw new Exception($"Cache contains null value for {key}");
        }
        return result;
    }

    private ConcurrentDictionary<T, byte> GetHash<T>(string key) where T : notnull
    {
        var result = _cache.GetOrCreate(key, x =>
        {
            _configOptions(x);
            return new ConcurrentDictionary<T, byte>();
        });
        if (result == null)
        {
            throw new Exception($"Cache contains null value for {key}");
        }
        return result;
    }

    private MemoryCacheEntryOptions GetMemoryCacheEntryOptions()
    {
        return new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(_cacheOptions.AbsoluteExpirationInSeconds))
            .SetSlidingExpiration(TimeSpan.FromSeconds(_cacheOptions.SlidingExpirationInSeconds));
    }

    public async Task<T?> GetOrCreate<T>(string key, Func<Task<T>> failover)
    {
        var result = await _cache.GetOrCreateAsync(key, async x =>
        {
            _configOptions(x);
            return await failover();
        });
        return result;
    }

    public void QueueSet<T>(string key, Func<T> getValue, Func<string> getField)
    {
        _queue.Add(() =>
        {
            Set(key, getValue(), getField());
            return Task.CompletedTask;
        });
    }

    public Task Remove(string key)
    {
        _cache.Remove(key);
        return Task.CompletedTask;
    }

    public Task Remove(IEnumerable<string> keys)
    {
        foreach (var key in keys)
        {
            _cache.Remove(key);
        }
        return Task.CompletedTask;
    }

    public Task Remove<T>(string key, IEnumerable<string> fields)
    {
        var dictionary = GetDictionary<T>(key);
        foreach (var field in fields)
        {
            dictionary.TryRemove(field, out _);
        }
        return Task.CompletedTask;
    }

    public Task Set<T>(IEnumerable<CacheObjectField<T>> objects)
    {
        foreach (var obj in objects)
        {
            Set(obj.Key, obj.Value, obj.Field);
        }
        return Task.CompletedTask;
    }

    public Task SRemove<T>(string key, T value) where T : notnull
    {
        var hash = GetHash<T>(key);
        hash.TryRemove(value, out _);
        return Task.CompletedTask;
    }

    public Task SSet<T>(string key, T value) where T : notnull
    {
        var options = GetMemoryCacheEntryOptions();
        _cache.Set(key, value, options);
        return Task.CompletedTask;
    }

    public Task Set<T>(string key, T value, string field)
    {
        var dictionary = GetDictionary<T>(key);
        dictionary.AddOrUpdate(field, value, (key, v) => value);
        return Task.CompletedTask;
    }

    public Task Set<T>(string key, T value)
    {
        var options = GetMemoryCacheEntryOptions();
        _cache.Set(key, value, options);
        return Task.CompletedTask;
    }
}
