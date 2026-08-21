using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Invoking;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.Services.Commands.InvoiceDetail;

public interface IInvoiceDetailInvoker
{
    Task<WPFResult> Get(InvokerToken token, int headerId);
    Task<WPFResult> Update(InvokerToken token, int headerId, IEnumerable<InvoiceDetailUpdateDTO> updates);
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

    public Task<WPFResult> Get(InvokerToken token, int headerId)
    {
        var command = new InvoiceDetailGetCommand(_service, _state, headerId);
        return _invoker.Perform(token, command);
    }

    public Task<WPFResult> Update(InvokerToken token, int headerId, IEnumerable<InvoiceDetailUpdateDTO> updates)
    {
        var command = new InvoiceDetailUpdateCommand(_service, _state, headerId, updates);
        return _invoker.Perform(token, command);
    }
}
