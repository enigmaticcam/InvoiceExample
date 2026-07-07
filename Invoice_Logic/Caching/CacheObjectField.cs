namespace Invoice_Logic.Caching;

public class CacheObjectField<T>
{
    public CacheObjectField(string key, string field, T value)
    {
        Key = key;
        Field = field;
        Value = value;
    }

    public string Key { get; set; }
    public string Field { get; set; }
    public T Value { get; set; }
}


