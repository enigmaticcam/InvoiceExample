using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.ServerCommand.InvoiceDetail;

public interface IInvoiceDetailInvoker
{
    Task<BlazorResult> GetResults(BroadcastToken token, int headerId);
    Task<BlazorResult> Update(BroadcastToken token, int headerId, IEnumerable<InvoiceDetailUpdateDTO> updates);
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

    public Task<BlazorResult> Update(BroadcastToken token, int headerId, IEnumerable<InvoiceDetailUpdateDTO> updates)
    {
        var command = new InvoiceDetailUpdate(_service, _state, headerId, updates);
        return _invoker.Perform(command, token);
    }
}
