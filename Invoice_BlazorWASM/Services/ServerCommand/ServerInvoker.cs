using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.ServerCommand;

public interface IServerInvoker
{
    Task<BlazorResult> Perform(IServerCommand<BlazorResult> command, BroadcastToken token);
    Task<BlazorResult<T>> Perform<T>(IServerCommand<BlazorResult<T>> command, BroadcastToken token);
}

public class ServerInvoker : IServerInvoker
{
    private IServerStatus _server;

    public ServerInvoker(IServerStatus server)
    {
        _server = server;
    }

    public async Task<BlazorResult> Perform(IServerCommand<BlazorResult> command, BroadcastToken token)
    {
        await _server.StartTask(token);
        BlazorResult? result = null;
        try
        {
            result = await command.Execute();
        }
        catch (Exception ex)
        {
            result = BlazorResult.Fail(ex.Message);
        }
        finally
        {
            if (result == null)
            {
                result = BlazorResult.Fail("An uncaught exception was thrown in ServerInvoker");
            }
            await _server.EndTask(token, result);
        }
        return result;
    }

    public async Task<BlazorResult<T>> Perform<T>(IServerCommand<BlazorResult<T>> command, BroadcastToken token)
    {
        await _server.StartTask(token);
        BlazorResult<T>? result = null;
        try
        {
            result = await command.Execute();
        }
        catch (Exception ex)
        {
            result = BlazorResult<T>.Fail(ex.Message);
        }
        finally
        {
            if (result == null)
            {
                result = BlazorResult<T>.Fail("An uncaught exception was thrown in ServerInvoker");
            }
            await _server.EndTask(token, result);
        }
        return result;
    }
}
