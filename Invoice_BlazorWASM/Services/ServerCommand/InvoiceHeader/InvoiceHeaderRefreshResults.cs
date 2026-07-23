using Invoice_BlazorWASM.Data;
using Invoice_BlazorWASM.Services.Core;
using Invoice_BlazorWASM.Services.ServerCommand.InvoiceDetail;

namespace Invoice_BlazorWASM.Services.ServerCommand.InvoiceHeader;

public class InvoiceHeaderRefreshResults : IServerCommand<BlazorResult>
{
    private IServiceWrapper _service;
    private IInvoiceDetailState _state;
    private int _headerId;

    public InvoiceHeaderRefreshResults(IServiceWrapper service, IInvoiceDetailState state, int headerId)
    {
        _service = service;
        _state = state;
        _headerId = headerId;
    }

    public async Task<BlazorResult> Execute()
    {
        var result = await _service.InvoiceHeader_RefreshResults(_headerId);
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Set(result.Obj.Select(x => new DTO_InvoiceDetail(x)));
        }
        return result;
    }
}
