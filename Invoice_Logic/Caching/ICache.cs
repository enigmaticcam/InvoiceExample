namespace Invoice_Logic.Caching;

public interface ICache
{
    Task<List<T>> Get<T>(string key, IEnumerable<string> fields);
    Task<T?> GetOrCreate<T>(string key, Func<Task<T>> failover);
    void QueueSet<T>(string key, Func<T> getValue, Func<string> getField);
    Task Set<T>(IEnumerable<CacheObjectField<T>> objects);
    Task SSet<T>(string key, T value) where T : notnull;
}
