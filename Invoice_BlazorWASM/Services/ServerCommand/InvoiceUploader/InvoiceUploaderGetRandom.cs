using Invoice_BlazorWASM.Services.Core;
using Invoice_BlazorWASM.Services.ServerCommand;

namespace Invoice_BlazorWASM.Services.ServerCommand.InvoiceUploader;

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
