using Invoice_Logic.Caching;

namespace Invoice_Logic.Repositories;

public abstract class CacheEntity<TId, TObject> where TId : notnull
{
    private ICache _cache;

    protected CacheEntity(ICache cache)
    {
        _cache = cache;
    }

    protected abstract Task<List<TObject>> GetFromEntity(IEnumerable<TId> ids);
    protected abstract TId GetId(TObject obj);
    protected abstract string ObjectKey { get; }
    private string ListKey => $"{ObjectKey}_List";

    protected Task CacheClear(IEnumerable<TId> ids)
    {
        return _cache.Remove<TObject>(ObjectKey, ids.Select(x => x.ToString() ?? ""));
    }

    protected async Task CacheClearList(string listKey)
    {
        await _cache.Remove(listKey);
        await _cache.SRemove(ListKey, listKey);
    }

    protected void CacheQueueSet(Func<TObject> getObj)
    {
        _cache.QueueSet<TObject>(ObjectKey, getObj, () => GetId(getObj()).ToString() ?? "");
    }

    protected async Task<TObject> GetFromCache(TId id)
    {
        var ids = new List<TId>() { id };
        var value = await GetFromCache(ids);
        return value.First();
    }

    protected async Task<List<TObject>> GetFromCache(IEnumerable<TId> ids)
    {
        var stringIds = ids
            .Select(x => new
            {
                Value = x,
                StringValue = x.ToString() ?? ""
            })
            .ToList();
        var result = await _cache.Get<TObject>(ObjectKey, stringIds.Select(x => x.StringValue));
        var diff = stringIds.ExceptBy(result.Select(x => GetId(x).ToString()), x => x.StringValue);
        if (diff.Count() > 0)
        {
            var fromEntity = await GetFromEntity(diff.Select(x => x.Value));
            await _cache.Set(fromEntity.Select(x => new CacheObjectField<TObject>(ObjectKey, GetId(x).ToString() ?? "", x)));
            result.AddRange(fromEntity);
        }
        return result;
    }

    protected async Task<List<TObject>> GetFromCache(string listKey, Func<Task<List<TId>>> GetFromEntity)
    {
        var list = await _cache.GetOrCreate(listKey, async () =>
        {
            await _cache.SSet(ListKey, listKey);
            return await GetFromEntity();
        });
        if (list == null)
        {
            throw new Exception($"Cache contains null value for List Key {listKey}");
        }
        return await GetFromCache(list);
    }

    protected async Task<List<TId>> GetList(string listKey, Func<Task<List<TId>>> getFromEntity)
    {
        var list = await _cache.GetOrCreate(listKey, async () =>
        {
            await _cache.SSet(ListKey, listKey);
            return await getFromEntity();
        });
        if (list == null)
        {
            throw new Exception($"Cache List for {listKey} is null");
        }
        return list;
    }
}
