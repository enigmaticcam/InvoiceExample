using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Invoking;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.Services.Commands.ResultStatusType;

public interface IResultStatusInvoker
{
    Task<WPFResult> Get(InvokerToken token);
}

public class ResultStatusInvoker : IResultStatusInvoker
{
    private IServerInvoker _invoker;
    private IServiceWrapper _service;
    private IResultStatusTypeState _state;

    public ResultStatusInvoker(IServerInvoker invoker, IServiceWrapper service, IResultStatusTypeState state)
    {
        _invoker = invoker;
        _service = service;
        _state = state;
    }

    public Task<WPFResult> Get(InvokerToken token)
    {
        var command = new ResultStatusTypeGetCommand(_service, _state);
        return _invoker.Perform(token, command);
    }
}
