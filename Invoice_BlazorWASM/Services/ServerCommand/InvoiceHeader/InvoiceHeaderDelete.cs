using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.ServerCommand.InvoiceHeader;

public class InvoiceHeaderDelete : IServerCommand<BlazorResult>
{
    private IServiceWrapper _service;
    private IInvoiceHeaderState _state;
    private int _headerId;

    public InvoiceHeaderDelete(IServiceWrapper service, IInvoiceHeaderState state, int headerId)
    {
        _service = service;
        _state = state;
        _headerId = headerId;
    }

    public async Task<BlazorResult> Execute()
    {
        var result = await _service.InvoiceHeader_Delete(_headerId);
        if (result.IsSuccess)
        {
            await _state.Remove(_headerId);
        }
        return result;
    }
}
