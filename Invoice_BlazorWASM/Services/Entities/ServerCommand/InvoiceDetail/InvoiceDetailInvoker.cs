using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.Entities.ServerCommand.InvoiceDetail;

public interface IInvoiceDetailInvoker
{
    Task<BlazorResult> GetResults(BroadcastToken token, int headerId);
}

public class InvoiceDetailInvoker : IInvoiceDetailInvoker
{
    private IServerInvoker _invoker;
    private IServiceWrapper _service;
    private IInvoiceDetailState _state;

    public InvoiceDetailInvoker(IServerInvoker invoker, IServiceWrapper service, IInvoiceDetailState state)
    {
        _invoker = invoker;
        _service = service;
        _state = state;
    }

    public Task<BlazorResult> GetResults(BroadcastToken token, int headerId)
    {
        var command = new InvoiceDetailGetResults(_service, _state, headerId);
        return _invoker.Perform(command, token);
    }
}
