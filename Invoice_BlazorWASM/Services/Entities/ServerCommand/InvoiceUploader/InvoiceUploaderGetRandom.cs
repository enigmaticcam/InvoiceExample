using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.Entities.ServerCommand.InvoiceUploader;

public class InvoiceUploaderGetRandom : IServerCommand<BlazorResult<string>>
{
    private IServiceWrapper _service;

    public InvoiceUploaderGetRandom(IServiceWrapper service)
    {
        _service = service;
    }

    public Task<BlazorResult<string>> Execute()
    {
        return _service.InvoiceUploader_GetRandom();
    }
}
