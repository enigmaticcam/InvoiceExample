using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.Services.Commands.InvoiceHeader;

public class InvoiceHeaderChangeStatusCommand : IServerCommand<WPFResult<InvoiceUpdateResultDTO>>
{
    private IServiceWrapper _service;
    private IInvoiceHeaderState _state;
    private int _headerId;
    private int _statusId;

    public InvoiceHeaderChangeStatusCommand(IServiceWrapper service, IInvoiceHeaderState state, int headerId, int statusId)
    {
        _service = service;
        _state = state;
        _headerId = headerId;
        _statusId = statusId;
    }

    public async Task<WPFResult<InvoiceUpdateResultDTO>> Execute()
    {
        var result = await _service.InvoiceHeader_Update(_headerId, _statusId);
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Merge(result.Obj.Invoice);
        }
        return result;
    }
}
