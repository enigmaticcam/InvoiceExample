using Invoice_BlazorWASM.Data;
using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.ServerCommand.InvoiceDetail;

public class InvoiceDetailUpdate : IServerCommand<BlazorResult>
{
    private IServiceWrapper _service;
    private IInvoiceDetailState _state;
    private int _headerId;
    private IEnumerable<InvoiceDetailUpdateDTO> _updates;

    public InvoiceDetailUpdate(IServiceWrapper service, IInvoiceDetailState state, int headerId, IEnumerable<InvoiceDetailUpdateDTO> updates)
    {
        _service = service;
        _state = state;
        _headerId = headerId;
        _updates = updates;
    }

    public async Task<BlazorResult> Execute()
    {
        var result = await _service.InvoiceHeader_Update(_headerId, _updates);
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Set(result.Obj.Select(x => new DTO_InvoiceDetail(x)));
        }
        return result;
    }
}
