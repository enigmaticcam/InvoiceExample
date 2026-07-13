using Invoice_Logic.Data.DTOs.Entity;

namespace Invoice_Logic.Core.Interfaces;

public interface IInvoiceUploaderCore
{
    Task<List<InvoiceHeaderEntity>> Get();
    Task<string> GetRandom();
    Task<List<InvoiceHeaderEntity>> Create(Stream stream);
}
