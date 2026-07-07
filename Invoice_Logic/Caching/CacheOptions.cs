namespace Invoice_Logic.Caching;

public class CacheOptions
{
    public int SlidingExpirationInSeconds { get; set; }
    public int AbsoluteExpirationInSeconds { get; set; }
}
