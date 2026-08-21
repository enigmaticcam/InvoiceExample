using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.Services.Commands.InvoiceDetail;

public class InvoiceDetailUpdateCommand : IServerCommand<WPFResult>
{
    private IServiceWrapper _service;
    private IInvoiceDetailState _state;
    private int _headerId;
    private IEnumerable<InvoiceDetailUpdateDTO> _updates;

    public InvoiceDetailUpdateCommand(IServiceWrapper service, IInvoiceDetailState state, int headerId, IEnumerable<InvoiceDetailUpdateDTO> updates)
    {
        _service = service;
        _state = state;
        _headerId = headerId;
        _updates = updates;
    }

    public async Task<WPFResult> Execute()
    {
        var result = await _service.InvoiceHeader_Update(_headerId, _updates);
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Merge(result.Obj.Select(x => new Models.InvoiceFullResultModel(x)));
        }
        return result;
    }
}
