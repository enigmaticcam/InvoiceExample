using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.Services.Commands.InvoiceUploader;

public class InvoiceUploaderGetCommand : IServerCommand<WPFResult<List<InvoiceHeaderEntity>>>
{
    private IServiceWrapper _service;
    private IInvoiceUploaderState _state;

    public InvoiceUploaderGetCommand(IServiceWrapper service, IInvoiceUploaderState state)
    {
        _service = service;
        _state = state;
    }

    public async Task<WPFResult<List<InvoiceHeaderEntity>>> Execute()
    {
        var result = await _service.InvoiceUploader_Get();
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Set(result.Obj);
        }
        return result;
    }
}
