namespace Invoice_Logic.API;

public class APIResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = "";
    public long Time { get; set; }

    public static APIResult Successful() => new APIResult()
    {
        Message = "",
        Success = true
    };

    public static APIResult Failure(string message) => new APIResult()
    {
        Message = message,
        Success = false
    };
}

public class APIResult<T> : APIResult
{
    public T? Obj { get; set; }

    public static APIResult<T> Successful(T obj)
    {
        return Successful(obj, "");
    }

    public static APIResult<T> Successful(T obj, string message)
    {
        return new APIResult<T>()
        {
            Message = message,
            Obj = obj,
            Success = true
        };
    }

    public static new APIResult<T> Failure(string message)
    {
        return new APIResult<T>()
        {
            Message = message,
            Obj = default(T),
            Success = false
        };
    }
}
