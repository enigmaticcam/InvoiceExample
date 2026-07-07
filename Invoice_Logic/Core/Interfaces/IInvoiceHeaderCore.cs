using Invoice_Logic.Data.DTOs.Entity;

namespace Invoice_Logic.Core.Interfaces;

public interface IInvoiceHeaderCore
{
    Task<InvoiceHeaderEntity> Get(int id);
    Task<List<InvoiceDetailEntity>> GetDetail(int id);
}
