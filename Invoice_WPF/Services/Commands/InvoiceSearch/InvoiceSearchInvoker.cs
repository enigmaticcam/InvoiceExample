using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Entities;
using Invoice_WPF.Services.Invoking;

namespace Invoice_WPF.Services.Commands.InvoiceSearch;

public interface IInvoiceSearchInvoker
{
    Task<WPFResult> Search(InvokerToken token);
    Task<WPFResult> Search(InvokerToken token, InvoiceFilterDTO filter);
}

public class InvoiceSearchInvoker : IInvoiceSearchInvoker
{
    private IServerInvoker _invoker;
    private IServiceWrapper _service;
    private IInvoiceSearchState _state;

    public InvoiceSearchInvoker(IServerInvoker invoker, IServiceWrapper service, IInvoiceSearchState state)
    {
        _invoker = invoker;
        _service = service;
        _state = state;
    }

    public Task<WPFResult> Search(InvokerToken token)
    {
        var command = new InvoiceSearchGetCommand(_service, _state);
        return _invoker.Perform(token, command);
    }

    public Task<WPFResult> Search(InvokerToken token, InvoiceFilterDTO filter)
    {
        var command = new InvoiceSearchGetCommand(_service, _state, filter);
        return _invoker.Perform(token, command);
    }
}
