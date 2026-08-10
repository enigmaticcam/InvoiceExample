using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.Services.Commands.InvoiceHeader;

public class InvoiceHeaderGetCommand : IServerCommand<WPFResult>
{
    private IServiceWrapper _service;
    private IInvoiceHeaderState _state;
    private int _headerId;

    public InvoiceHeaderGetCommand(IServiceWrapper service, IInvoiceHeaderState state, int headerId)
    {
        _service = service;
        _state = state;
        _headerId = headerId;
    }

    public async Task<WPFResult> Execute()
    {
        var result = await _service.InvoiceHeader_Get(_headerId);
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Merge(result.Obj);
        }
        return result;
    }
}
