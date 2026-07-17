using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.Entities.ServerCommand.InvoiceUploader;

public interface IInvoiceUploaderInvoker
{
    Task<BlazorResult> Get(BroadcastToken token);
    Task<BlazorResult<RandomInvoiceDTO>> GetRandom(BroadcastToken token);
    Task<BlazorResult<List<InvoiceHeaderEntity>>> Upload(BroadcastToken token, Stream stream);
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

    public Task<BlazorResult> Get(BroadcastToken token)
    {
        var command = new InvoiceUploaderGet(_service, _state);
        return _invoker.Perform(command, token);
    }

    public Task<BlazorResult<RandomInvoiceDTO>> GetRandom(BroadcastToken token)
    {
        var command = new InvoiceUploaderGetRandom(_service);
        return _invoker.Perform(command, token);
    }

    public Task<BlazorResult<List<InvoiceHeaderEntity>>> Upload(BroadcastToken token, Stream stream)
    {
        var command = new InvoiceUploaderUpload(_service, _state, stream);
        return _invoker.Perform(command, token);
    }
}
