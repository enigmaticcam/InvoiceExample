using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.Entities.ServerCommand.InvoiceUploader;

public class InvoiceUploaderGetRandom : IServerCommand<BlazorResult<RandomInvoiceDTO>>
{
    private IServiceWrapper _service;

    public InvoiceUploaderGetRandom(IServiceWrapper service)
    {
        _service = service;
    }

    public Task<BlazorResult<RandomInvoiceDTO>> Execute()
    {
        return _service.InvoiceUploader_GetRandom();
    }
}
