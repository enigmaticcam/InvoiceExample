using Invoice_Logic.Data.DTOs;

namespace Invoice_Logic.Core.Interfaces;

public interface IInvoiceSearchCore
{
    Task<InvoiceSearchDTO> Get();
    Task<InvoiceSearchDTO> Get(InvoiceFilterDTO filter);
}
