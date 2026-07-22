using Invoice_Logic.Data.DTOs.Entity;

namespace Invoice_Logic.Repositories.DbEntities.Interfaces;

public interface IInvoiceResultDbEntity
{
    Task<List<int>> Get(int headerId);
    Task<List<InvoiceResultEntity>> Get(IEnumerable<int> ids);
}
