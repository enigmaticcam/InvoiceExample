using Invoice_BlazorWASM.Services.Core;
using Invoice_BlazorWASM.Services.ServerCommand;

namespace Invoice_BlazorWASM.Services.ServerCommand.InvoiceSearch;

public interface IInvoiceSearchInvoker
{
    Task<BlazorResult<InvoiceSearchDTO>> Get(BroadcastToken token);
    Task<BlazorResult<InvoiceSearchDTO>> Get(BroadcastToken token, InvoiceFilterDTO filter);
}

public class InvoiceSearchInvoker : IInvoiceSearchInvoker
{
    private IServerInvoker _invoker;
    private IServiceWrapper _service;

    public InvoiceSearchInvoker(IServerInvoker invoker, IServiceWrapper service)
    {
        _invoker = invoker;
        _service = service;
    }

    public Task<BlazorResult<InvoiceSearchDTO>> Get(BroadcastToken token)
    {
        var command = new InvoiceSearchGet(_service);
        return _invoker.Perform(command, token);
    }

    public Task<BlazorResult<InvoiceSearchDTO>> Get(BroadcastToken token, InvoiceFilterDTO filter)
    {
        var command = new InvoiceSearchGetWithFilter(_service, filter);
        return _invoker.Perform(command, token);
    }
}
