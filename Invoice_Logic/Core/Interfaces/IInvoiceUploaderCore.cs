using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Data.DTOs.Entity;

namespace Invoice_Logic.Core.Interfaces;

public interface IInvoiceUploaderCore
{
    Task<List<InvoiceHeaderEntity>> Get();
    string GetBlankTemplate();
    Task<RandomInvoiceDTO> GetRandom();
    Task<List<InvoiceHeaderEntity>> Create(Stream stream);
}
