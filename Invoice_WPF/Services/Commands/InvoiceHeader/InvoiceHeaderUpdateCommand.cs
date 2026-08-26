using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.Services.Commands.InvoiceHeader;

public class InvoiceHeaderUpdateCommand : IServerCommand<WPFResult>
{
    private IServiceWrapper _service;
    private IInvoiceHeaderState _state;
    private int _headerId;
    private InvoiceHeaderUpdateDTO _update;

    public InvoiceHeaderUpdateCommand(IServiceWrapper service, IInvoiceHeaderState state, int headerId, InvoiceHeaderUpdateDTO update)
    {
        _service = service;
        _state = state;
        _headerId = headerId;
        _update = update;
    }

    public async Task<WPFResult> Execute()
    {
        var result = await _service.InvoiceHeader_Update(_headerId, _update);
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Merge(new Models.InvoiceHeaderModel(result.Obj));
        }
        return result;
    }
}
