using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.ServerCommand.StatusType;

public interface IStatusTypeInvoker
{
    Task<BlazorResult> Get(BroadcastToken token);
}

public class StatusTypeInvoker : IStatusTypeInvoker
{
    private IServerInvoker _invoker;
    private IServiceWrapper _service;
    private IStatusTypeState _state;

    public StatusTypeInvoker(IServerInvoker invoker, IServiceWrapper service, IStatusTypeState state)
    {
        _invoker = invoker;
        _service = service;
        _state = state;
    }

    public Task<BlazorResult> Get(BroadcastToken token)
    {
        var command = new StatusTypeGet(_service, _state);
        return _invoker.Perform(command, token);
    }
}
