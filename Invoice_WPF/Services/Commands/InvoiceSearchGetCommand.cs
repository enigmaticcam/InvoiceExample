using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Entities;

namespace Invoice_WPF.Services.Commands
{
    public class InvoiceSearchGetCommand
    {
        private IServiceWrapper _service;
        private IInvoiceSearchState _state;

        public InvoiceSearchGetCommand(IServiceWrapper service, IInvoiceSearchState state)
        {
            _service = service;
            _state = state;
        }

        public async Task Perform()
        {
            var result = await _service.InvoiceSearch_Get();
            if (result.IsSuccess && result.Obj != null)
            {
                await _state.Set(result.Obj);
            }
        }

        public async Task Perform(InvoiceFilterDTO filter)
        {
            var result = await _service.InvoiceSearch_Get(filter);
            if (result.IsSuccess && result.Obj != null)
            {
                await _state.Set(result.Obj);
            }
        }
    }
}
