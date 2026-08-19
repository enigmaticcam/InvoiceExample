using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.States;
using System.IO;

namespace Invoice_WPF.Services.Commands.InvoiceUploader;

public class InvoiceUploaderUploadCommand : IServerCommand<WPFResult<List<InvoiceHeaderEntity>>>
{
    private IServiceWrapper _service;
    private IInvoiceUploaderState _state;
    private string _file;

    public InvoiceUploaderUploadCommand(IServiceWrapper service, IInvoiceUploaderState state, string file)
    {
        _service = service;
        _state = state;
        _file = file;
    }

    public async Task<WPFResult<List<InvoiceHeaderEntity>>> Execute()
    {
        var stream = new FileStream(_file, FileMode.Open, FileAccess.Read);
        var result = await _service.InvoiceUploader_Upload(new FileParameter(stream));
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Set(result.Obj);
        }
        return result;
    }
}
