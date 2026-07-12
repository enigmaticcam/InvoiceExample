using Invoice_Logic.Data.EF;

namespace Invoice_Logic.API.Pipelines;

public interface IExceptionHandler
{
    Task<Exception> LogException(Exception exception);
}

public class ExceptionHandler : IExceptionHandler
{
    private Invoice_Context _context;

    public ExceptionHandler(Invoice_Context context)
    {
        _context = context;
    }

    public async Task<Exception> LogException(Exception exception)
    {
        exception = GetInnerException(exception);
        var log = new ExceptionLog()
        {
            LogMessage = exception.Message ?? "",
            LogStackTrace = exception.StackTrace ?? ""
        };
        _context.ChangeTracker.Clear();
        await _context.AddAsync(log);
        await _context.SaveChangesAsync();
        return exception;
    }

    private Exception GetInnerException(Exception ex)
    {
        while (ex.InnerException != null)
        {
            ex = ex.InnerException;
        }
        return ex;
    }
}
