namespace Invoice_WPF.Services.Core;

public class WPFResult
{
    public static WPFResult Success() => new WPFResult()
    {
        IsSuccess = true
    };

    public static WPFResult Success(string message) => new WPFResult()
    {
        IsSuccess = true,
        Message = message
    };

    public static WPFResult Fail(string message) => new WPFResult()
    {
        Message = message
    };

    public bool IsSuccess { get; set; }
    public string Message { get; set; } = "";
}

public class WPFResult<T> : WPFResult
{
    public static WPFResult<T> Success(T obj) => new WPFResult<T>()
    {
        IsSuccess = true,
        Obj = obj
    };

    public static WPFResult<T> Success(T obj, string message) => new WPFResult<T>()
    {
        IsSuccess = true,
        Obj = obj,
        Message = message
    };

    public new static WPFResult<T> Fail(string message) => new WPFResult<T>()
    {
        Message = message
    };

    public T? Obj { get; set; }
}
