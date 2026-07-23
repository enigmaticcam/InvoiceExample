using Invoice_BlazorWASM.Data;
using Invoice_BlazorWASM.Services.Core;
using Invoice_BlazorWASM.Services.ServerCommand;

namespace Invoice_BlazorWASM.Services.ServerCommand.InvoiceUploader;

public class InvoiceUploaderUpload : IServerCommand<BlazorResult<List<InvoiceHeaderEntity>>>
{
    private IServiceWrapper _service;
    private IInvoiceUploaderState _state;
    private Stream _stream;

    public InvoiceUploaderUpload(IServiceWrapper service, IInvoiceUploaderState state, Stream stream)
    {
        _service = service;
        _state = state;
        _stream = stream;
    }

    public async Task<BlazorResult<List<InvoiceHeaderEntity>>> Execute()
    {
        var result = await _service.InvoiceUploader_Upload(new FileParameter(_stream));
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Set(result.Obj.Select(x => new DTO_InvoiceHeader(x)));
        }
        return result;
    }
}
