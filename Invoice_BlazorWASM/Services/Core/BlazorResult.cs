namespace Invoice_BlazorWASM.Services.Core;

public class BlazorResult
{
    public static BlazorResult Success() => new BlazorResult()
    {
        IsSuccess = true
    };

    public static BlazorResult Success(string message) => new BlazorResult()
    {
        IsSuccess = true,
        Message = message
    };

    public static BlazorResult Fail(string message) => new BlazorResult()
    {
        Message = message
    };

    public bool IsSuccess { get; set; }
    public string Message { get; set; } = "";
}

public class BlazorResult<T> : BlazorResult
{
    public static BlazorResult<T> Success(T obj) => new BlazorResult<T>()
    {
        IsSuccess = true,
        Obj = obj
    };

    public static BlazorResult<T> Success(T obj, string message) => new BlazorResult<T>()
    {
        IsSuccess = true,
        Obj = obj,
        Message = message
    };

    public new static BlazorResult<T> Fail(string message) => new BlazorResult<T>()
    {
        Message = message
    };

    public T? Obj { get; set; }
}



