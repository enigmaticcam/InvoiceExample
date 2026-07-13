using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.Entities.ServerCommand.InvoiceSearch;

public class InvoiceSearchGet : IServerCommand<BlazorResult<InvoiceSearchDTO>>
{
    private IServiceWrapper _service;

    public InvoiceSearchGet(IServiceWrapper service)
    {
        _service = service;
    }

    public async Task<BlazorResult<InvoiceSearchDTO>> Execute()
    {
        var result = await _service.InvoiceSearch_Get();
        return result;
    }
}
