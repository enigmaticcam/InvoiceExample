using Invoice_Avalonia.Services.Core;
using System.Threading.Tasks;

namespace Invoice_Avalonia.Command.InvoiceSearch;

public class SearchInvoiceCommand
{
    private IServiceWrapper _serviceWrapper;
    private InvoiceFilterDTO _filter;

    public SearchInvoiceCommand(IServiceWrapper serviceWrapper, InvoiceFilterDTO filter)
    {
        _serviceWrapper = serviceWrapper;
        _filter = filter;
    }

    public Task<WebResult<InvoiceSearchDTO>> Execute()
    {
        return _serviceWrapper.InvoiceSearch_Get(_filter);
    }
}
