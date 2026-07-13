using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.Entities.ServerCommand.InvoiceSearch;

public interface IInvoiceSearchInvoker
{
    Task<BlazorResult<InvoiceSearchDTO>> Get(BroadcastToken token);
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
}
