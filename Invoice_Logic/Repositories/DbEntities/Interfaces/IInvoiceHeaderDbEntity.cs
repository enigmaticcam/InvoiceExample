using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Data.DTOs.Update;

namespace Invoice_Logic.Repositories.DbEntities.Interfaces;

public interface IInvoiceHeaderDbEntity
{
    Task<LateLoader<int, InvoiceHeaderEntity>> Create(InvoiceHeaderCreateDTO create);
    Task Delete(int id);
    Task<List<InvoiceHeaderEntity>> Get(IEnumerable<int> ids);
    Task<List<int>> Get(InvoiceFilterDTO filter);
    Task<LateLoader<InvoiceHeaderEntity>> Update(int headerId, int statusTypeId);
    Task<LateLoader<InvoiceHeaderEntity>> Update(int headerId, InvoiceHeaderUpdateDTO update);
}
