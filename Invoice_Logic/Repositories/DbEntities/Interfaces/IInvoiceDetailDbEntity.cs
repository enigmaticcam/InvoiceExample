using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Data.DTOs.Update;

namespace Invoice_Logic.Repositories.DbEntities.Interfaces;

public interface IInvoiceDetailDbEntity
{
    Task Create(int headerId, IEnumerable<InvoiceDetailCreateDTO> creates);
    Task<List<int>> Delete(int headerId);
    Task<List<int>> Get(int headerId);
    Task<List<InvoiceDetailEntity>> Get(IEnumerable<int> ids);
    Task Update(int headerId, IEnumerable<InvoiceDetailUpdateDTO> updates);
}
