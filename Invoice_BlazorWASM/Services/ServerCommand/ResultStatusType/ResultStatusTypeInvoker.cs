using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.ServerCommand.ResultStatusType;

public interface IResultStatusTypeInvoker
{
    Task<BlazorResult> Get(BroadcastToken token);
}

public class ResultStatusTypeInvoker : IResultStatusTypeInvoker
{
    private IServerInvoker _invoker;
    private IServiceWrapper _service;
    private IResultStatusTypeState _state;

    public ResultStatusTypeInvoker(IServerInvoker invoker, IServiceWrapper service, IResultStatusTypeState state)
    {
        _invoker = invoker;
        _service = service;
        _state = state;
    }

    public Task<BlazorResult> Get(BroadcastToken token)
    {
        var command = new ResultStatusTypeGet(_service, _state);
        return _invoker.Perform(command, token);
    }
}
