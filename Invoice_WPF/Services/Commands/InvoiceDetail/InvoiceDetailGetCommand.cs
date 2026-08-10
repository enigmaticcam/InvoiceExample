using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.Services.Commands.InvoiceDetail;

public class InvoiceDetailGetCommand : IServerCommand<WPFResult>
{
    private IServiceWrapper _service;
    private IInvoiceDetailState _state;
    private int _headerId;

    public InvoiceDetailGetCommand(IServiceWrapper service, IInvoiceDetailState state, int headerId)
    {
        _service = service;
        _state = state;
        _headerId = headerId;
    }

    public async Task<WPFResult> Execute()
    {
        var result = await _service.InvoiceHeader_GetResults(_headerId);
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Set(result.Obj);
        }
        return result;
    }
}
