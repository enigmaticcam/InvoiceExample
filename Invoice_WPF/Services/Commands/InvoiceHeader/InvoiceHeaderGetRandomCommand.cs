using Invoice_WPF.Services.Core;

namespace Invoice_WPF.Services.Commands.InvoiceHeader;

public class InvoiceHeaderGetRandomCommand : IServerCommand<WPFResult<RandomInvoiceDTO>>
{
    private IServiceWrapper _service;

    public InvoiceHeaderGetRandomCommand(IServiceWrapper service)
    {
        _service = service;
    }

    public Task<WPFResult<RandomInvoiceDTO>> Execute()
    {
        return _service.InvoiceUploader_GetRandom();
    }
}
