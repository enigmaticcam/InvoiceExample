using Invoice_BlazorWASM.Data;
using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.ServerCommand.InvoiceHeader;

public class InvoiceHeaderUpdateStatus : IServerCommand<BlazorResult<InvoiceHeaderEntity>>
{
    private IServiceWrapper _service;
    private IInvoiceHeaderState _state;
    private int _headerId;
    private int _statusTypeId;

    public InvoiceHeaderUpdateStatus(IServiceWrapper service, IInvoiceHeaderState state, int headerId, int statusTypeId)
    {
        _service = service;
        _state = state;
        _headerId = headerId;
        _statusTypeId = statusTypeId;
    }

    public async Task<BlazorResult<InvoiceHeaderEntity>> Execute()
    {
        var result = await _service.InvoiceHeader_Update(_headerId, _statusTypeId);
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Merge(new DTO_InvoiceHeader(result.Obj));
        }
        return result;
    }
}
