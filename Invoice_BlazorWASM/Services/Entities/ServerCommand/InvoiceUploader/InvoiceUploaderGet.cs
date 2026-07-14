using Invoice_BlazorWASM.Data;
using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.Entities.ServerCommand.InvoiceUploader;

public class InvoiceUploaderGet : IServerCommand<BlazorResult>
{
    private IServiceWrapper _service;
    private IInvoiceUploaderState _state;

    public InvoiceUploaderGet(IServiceWrapper service, IInvoiceUploaderState state)
    {
        _service = service;
        _state = state;
    }

    public async Task<BlazorResult> Execute()
    {
        var result = await _service.InvoiceUploader_Get();
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Set(result.Obj.Select(x => new DTO_InvoiceHeader(x)).ToList());
        }
        return result;
    }
}
