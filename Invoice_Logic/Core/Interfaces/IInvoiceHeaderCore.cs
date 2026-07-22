using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Repositories;

namespace Invoice_Logic.Core.Interfaces;

public interface IInvoiceHeaderCore
{
    Task<InvoiceHeaderEntity> Get(int id);
    Task<List<InvoiceHeaderEntity>> Get(InvoiceFilterDTO filter);
    Task<List<InvoiceDetailEntity>> GetDetail(int id);
    Task<InvoicePermissionsDTO> GetPermissions(int id);
    Task<List<InvoiceFullResultDTO>> GetResults(int id);
    Task<LateLoader<int, InvoiceHeaderEntity>> QueueCreate(InvoiceHeaderCreateDTO create);
    Task QueueCreate(int headerId, IEnumerable<InvoiceDetailCreateDTO> creates);
    Task<List<InvoiceDetailEntity>> UpdateRefreshResults(int headerId);
}
