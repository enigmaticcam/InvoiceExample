using Invoice_BlazorWASM.Services.Entities;
using Invoice_WPF.Services.Core;

namespace Invoice_WPF.Services.Entities
{
    public interface IInvoiceSearchState : ISingleEntityState<InvoiceSearchDTO> { }


    public class InvoiceSearchState : SingleEntityState<InvoiceSearchDTO>, IInvoiceSearchState
    {
    }
}
