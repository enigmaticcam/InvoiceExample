using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.Entities.ServerCommand.InvoiceUploader;

public interface IInvoiceUploaderInvoker
{
    Task<BlazorResult<string>> GetRandom(BroadcastToken token);
}

public class InvoiceUploaderInvoker : IInvoiceUploaderInvoker
{
    private IServerInvoker _invoker;
    private IServiceWrapper _service;

    public InvoiceUploaderInvoker(IServerInvoker invoker, IServiceWrapper service)
    {
        _invoker = invoker;
        _service = service;
    }

    public Task<BlazorResult<string>> GetRandom(BroadcastToken token)
    {
        var command = new InvoiceUploaderGetRandom(_service);
        return _invoker.Perform(command, token);
    }
}
