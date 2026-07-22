using Invoice_BlazorWASM.Data;
using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.Entities.ServerCommand.InvoiceDetail;

public class InvoiceDetailGetResults : IServerCommand<BlazorResult>
{
    private IServiceWrapper _service;
    private IInvoiceDetailState _state;
    private int _headerId;

    public InvoiceDetailGetResults(IServiceWrapper service, IInvoiceDetailState state, int headerId)
    {
        _service = service;
        _state = state;
        _headerId = headerId;
    }

    public async Task<BlazorResult> Execute()
    {
        var result = await _service.InvoiceHeader_GetResults(_headerId);
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Set(result.Obj.Select(x => new DTO_InvoiceDetail(x)));
        }
        return result;
    }
}
