using Invoice_Logic.Data.DTOs.Entity;

namespace Invoice_Logic.Core.Interfaces;

public interface IInvoiceUploaderCore
{
    Task<List<InvoiceHeaderEntity>> Get();
    Task<List<InvoiceHeaderEntity>> Create(Stream stream);
}
