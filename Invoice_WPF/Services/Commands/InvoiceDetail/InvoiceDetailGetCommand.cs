using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.Services.Commands.InvoiceDetail;

public class InvoiceDetailGetCommand
{
    private IServiceWrapper _service;
    private IInvoiceDetailState _state;

    public InvoiceDetailGetCommand(IServiceWrapper service, IInvoiceDetailState state)
    {
        _service = service;
        _state = state;
    }

    public async Task<WPFResult> Perform(int headerId)
    {
        var result = await _service.InvoiceHeader_GetResults(headerId);
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Set(result.Obj);
        }
        return result;
    }
}
