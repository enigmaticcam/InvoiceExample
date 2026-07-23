using Invoice_BlazorWASM.Services.Core;
using Invoice_BlazorWASM.Services.ServerCommand;

namespace Invoice_BlazorWASM.Services.ServerCommand.InvoiceSearch;

public class InvoiceSearchGetWithFilter : IServerCommand<BlazorResult<InvoiceSearchDTO>>
{
    private IServiceWrapper _service;
    private InvoiceFilterDTO _filter;

    public InvoiceSearchGetWithFilter(IServiceWrapper service, InvoiceFilterDTO filter)
    {
        _service = service;
        _filter = filter;
    }

    public Task<BlazorResult<InvoiceSearchDTO>> Execute()
    {
        return _service.InvoiceSearch_Get(_filter);
    }
}
