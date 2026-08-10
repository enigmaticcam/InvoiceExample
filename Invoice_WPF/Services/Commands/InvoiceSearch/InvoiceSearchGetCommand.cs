using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Entities;

namespace Invoice_WPF.Services.Commands.InvoiceSearch;

public class InvoiceSearchGetCommand : IServerCommand<WPFResult>
{
    private IServiceWrapper _service;
    private IInvoiceSearchState _state;
    private InvoiceFilterDTO? _filter;

    public InvoiceSearchGetCommand(IServiceWrapper service, IInvoiceSearchState state)
    {
        _service = service;
        _state = state;
    }

    public InvoiceSearchGetCommand(IServiceWrapper service, IInvoiceSearchState state, InvoiceFilterDTO? filter)
    {
        _service = service;
        _state = state;
        _filter = filter;
    }

    public async Task<WPFResult> Execute()
    {
        if (_filter == null)
        {
            return await Perform();
        }
        else
        {
            return await Perform(_filter);
        }
    }

    private async Task<WPFResult> Perform()
    {
        var result = await _service.InvoiceSearch_Get();
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Set(result.Obj);
        }
        return result;
    }

    private async Task<WPFResult> Perform(InvoiceFilterDTO filter)
    {
        var result = await _service.InvoiceSearch_Get(filter);
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Set(result.Obj);
        }
        return result;
    }
}
