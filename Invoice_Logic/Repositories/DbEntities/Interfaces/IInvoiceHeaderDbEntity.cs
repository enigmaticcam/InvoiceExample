using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;

namespace Invoice_Logic.Repositories.DbEntities.Interfaces;

public interface IInvoiceHeaderDbEntity
{
    Task<LateLoader<int, InvoiceHeaderEntity>> Create(InvoiceHeaderCreateDTO create);
    Task<List<InvoiceHeaderEntity>> Get(IEnumerable<int> ids);
}
