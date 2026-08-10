using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Invoking;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.Services.Commands.StatusType;

public interface IStatusTypeInvoker
{
    Task<WPFResult> Get(InvokerToken token);
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

    public Task<WPFResult> Get(InvokerToken token)
    {
        var command = new StatusTypeGetCommand(_service, _state);
        return _invoker.Perform(token, command);
    }
}
