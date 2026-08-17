using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Invoking;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.Services.Commands.InvoiceUploader;

public interface IInvoiceUploaderInvoker
{
    Task<WPFResult> Get(InvokerToken token);
}

public class InvoiceUploaderInvoker : IInvoiceUploaderInvoker
{
    private IServerInvoker _invoker;
    private IServiceWrapper _service;
    private IInvoiceUploaderState _state;

    public InvoiceUploaderInvoker(IServerInvoker invoker, IServiceWrapper service, IInvoiceUploaderState state)
    {
        _invoker = invoker;
        _service = service;
        _state = state;
    }

    public Task<WPFResult> Get(InvokerToken token)
    {
        var command = new InvoiceUploaderGetCommand(_service, _state);
        return _invoker.Perform(token, command);
    }
}
