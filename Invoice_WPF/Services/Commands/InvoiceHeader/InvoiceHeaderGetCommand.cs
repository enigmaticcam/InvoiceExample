using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.Services.Commands.InvoiceHeader;

public class InvoiceHeaderGetCommand
{
    private IServiceWrapper _service;
    private IInvoiceHeaderState _state;

    public InvoiceHeaderGetCommand(IServiceWrapper service, IInvoiceHeaderState state)
    {
        _service = service;
        _state = state;
    }

    public async Task<WPFResult> Perform(int headerId)
    {
        var result = await _service.InvoiceHeader_Get(headerId);
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Merge(result.Obj);
        }
        return result;
    }
}
