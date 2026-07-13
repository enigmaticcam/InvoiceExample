using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.Entities.ServerCommand.InvoiceHeader;

public interface IInvoiceHeaderInvoker
{
    Task<BlazorResult> Get(BroadcastToken token, int headerId);
}

public class InvoiceHeaderInvoker : IInvoiceHeaderInvoker
{
    private IServerInvoker _invoker;
    private IServiceWrapper _service;
    private IInvoiceHeaderState _state;

    public InvoiceHeaderInvoker(IServerInvoker invoker, IServiceWrapper service, IInvoiceHeaderState state)
    {
        _invoker = invoker;
        _service = service;
        _state = state;
    }

    public Task<BlazorResult> Get(BroadcastToken token, int headerId)
    {
        var command = new InvoiceHeaderGet(_service, _state, headerId);
        return _invoker.Perform(command, token);
    }
}
