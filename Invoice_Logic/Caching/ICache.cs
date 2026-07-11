namespace Invoice_Logic.Caching;

public interface ICache
{
    Task CommitQueue();
    Task<List<T>> Get<T>(string key, IEnumerable<string> fields);
    Task<T?> GetOrCreate<T>(string key, Func<Task<T>> failover);
    void QueueSet<T>(string key, Func<T> getValue, Func<string> getField);
    Task Remove(string key);
    Task Remove(IEnumerable<string> keys);
    Task Remove<T>(string key, IEnumerable<string> fields);
    Task Set<T>(string key, T value);
    Task Set<T>(IEnumerable<CacheObjectField<T>> objects);
    Task SRemove<T>(string key, T value) where T : notnull;
    Task SSet<T>(string key, T value) where T : notnull;
}
