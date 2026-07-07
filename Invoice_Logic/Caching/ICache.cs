namespace Invoice_Logic.Caching;

public interface ICache
{
    Task<List<T>> Get<T>(string key, IEnumerable<string> fields);
    void QueueSet<T>(string key, Func<T> getValue, Func<string> getField);
    Task Set<T>(IEnumerable<CacheObjectField<T>> objects);
}
