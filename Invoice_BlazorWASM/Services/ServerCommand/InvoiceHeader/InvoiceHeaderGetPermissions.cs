using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.ServerCommand.InvoiceHeader;

public class InvoiceHeaderGetPermissions : IServerCommand<BlazorResult<InvoicePermissionsDTO>>
{
    private IServiceWrapper _service;
    private int _headerId;

    public InvoiceHeaderGetPermissions(IServiceWrapper service, int headerId)
    {
        _service = service;
        _headerId = headerId;
    }

    public Task<BlazorResult<InvoicePermissionsDTO>> Execute()
    {
        return _service.InvoiceHeader_GetPermissions(_headerId);
    }
}
