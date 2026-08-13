using Invoice_WPF.Services.Core;

namespace Invoice_WPF.Services.Commands.InvoiceHeader;

public class InvoiceHeaderGetPermissionsCommand : IServerCommand<WPFResult<InvoicePermissionsDTO>>
{
    private IServiceWrapper _service;
    private int _headerId;

    public InvoiceHeaderGetPermissionsCommand(IServiceWrapper service, int headerId)
    {
        _service = service;
        _headerId = headerId;
    }

    public Task<WPFResult<InvoicePermissionsDTO>> Execute()
    {
        return _service.InvoiceHeader_GetPermissions(_headerId);
    }
}
