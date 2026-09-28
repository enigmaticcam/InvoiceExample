namespace Invoice_Avalonia.Services.Core;

public class WebResult
{
    public static WebResult Success() => new WebResult()
    {
        IsSuccess = true
    };

    public static WebResult Success(string message) => new WebResult()
    {
        IsSuccess = true,
        Message = message
    };

    public static WebResult Fail(string message) => new WebResult()
    {
        Message = message
    };

    public bool IsSuccess { get; set; }
    public string Message { get; set; } = "";
}

public class WebResult<T> : WebResult
{
    public static WebResult<T> Success(T obj) => new WebResult<T>()
    {
        IsSuccess = true,
        Obj = obj
    };

    public static WebResult<T> Success(T obj, string message) => new WebResult<T>()
    {
        IsSuccess = true,
        Obj = obj,
        Message = message
    };

    public new static WebResult<T> Fail(string message) => new WebResult<T>()
    {
        Message = message
    };

    public T? Obj { get; set; }
}
