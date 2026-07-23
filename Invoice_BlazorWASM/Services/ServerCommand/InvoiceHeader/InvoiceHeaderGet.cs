using Invoice_BlazorWASM.Data;
using Invoice_BlazorWASM.Services.Core;
using Invoice_BlazorWASM.Services.ServerCommand;

namespace Invoice_BlazorWASM.Services.ServerCommand.InvoiceHeader;

public class InvoiceHeaderGet : IServerCommand<BlazorResult>
{
    private IServiceWrapper _service;
    private IInvoiceHeaderState _state;
    private int _headerId;

    public InvoiceHeaderGet(IServiceWrapper service, IInvoiceHeaderState state, int headerId)
    {
        _service = service;
        _state = state;
        _headerId = headerId;
    }

    public async Task<BlazorResult> Execute()
    {
        var result = await _service.InvoiceHeader_Get(_headerId);
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Merge(new DTO_InvoiceHeader(result.Obj));
        }
        return result;
    }
}
