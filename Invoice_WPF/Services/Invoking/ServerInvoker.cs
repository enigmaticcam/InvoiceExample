using Invoice_WPF.Services.Core;

namespace Invoice_WPF.Services.Invoking;

public interface IServerInvoker
{
    Task<WPFResult> Perform(InvokerToken token, IServerCommand<WPFResult> command);
    Task<WPFResult<T>> Perform<T>(InvokerToken token, IServerCommand<WPFResult<T>> command);
}

public class ServerInvoker : IServerInvoker
{
    private IServerStatus _status;

    public ServerInvoker(IServerStatus status)
    {
        _status = status;
    }

    public async Task<WPFResult> Perform(InvokerToken token, IServerCommand<WPFResult> command)
    {
        await _status.StartTask(token);
        WPFResult? result = null;
        try
        {
            result = await command.Execute();
        }
        catch (Exception ex)
        {
            result = WPFResult.Fail(ex.Message);
        }
        finally
        {
            if (result == null)
            {
                result = WPFResult.Fail("An uncaught exception was thrown");
            }
            await _status.EndTask(token, result);
        }
        return result;
    }

    public async Task<WPFResult<T>> Perform<T>(InvokerToken token, IServerCommand<WPFResult<T>> command)
    {
        await _status.StartTask(token);
        WPFResult<T>? result = null;
        try
        {
            result = await command.Execute();
        }
        catch (Exception ex)
        {
            result = WPFResult<T>.Fail(ex.Message);
        }
        finally
        {
            if (result == null)
            {
                result = WPFResult<T>.Fail("An uncaught exception was thrown");
            }
            await _status.EndTask(token, result);
        }
        return result;
    }
}
