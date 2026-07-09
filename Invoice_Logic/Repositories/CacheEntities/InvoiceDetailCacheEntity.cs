using Invoice_Logic.Caching;
using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Repositories.DbEntities.Interfaces;

namespace Invoice_Logic.Repositories.CacheEntities;

public interface IInvoiceDetailCacheEntity
{
    Task Create(int headerId, IEnumerable<InvoiceDetailCreateDTO> creates);
    Task<List<InvoiceDetailEntity>> Get(int headerId);
}

public class InvoiceDetailCacheEntity : CacheEntity<int, InvoiceDetailEntity>, IInvoiceDetailCacheEntity
{
    private IInvoiceDetailDbEntity _invoiceDetailDbEntity;
    public InvoiceDetailCacheEntity(ICache cache, IInvoiceDetailDbEntity invoiceDetailDbEntity) : base(cache)
    {
        _invoiceDetailDbEntity = invoiceDetailDbEntity;
    }

    protected override string ObjectKey => "InvoiceDetailObject";
    private string ListKey_ByHeader(int headerId) => $"InvoiceDetailList_ByHeader_{headerId}";

    public Task Create(int headerId, IEnumerable<InvoiceDetailCreateDTO> creates)
    {
        return _invoiceDetailDbEntity.Create(headerId, creates);
    }

    public Task<List<InvoiceDetailEntity>> Get(int headerId)
    {
        return GetFromCache(ListKey_ByHeader(headerId), () => _invoiceDetailDbEntity.Get(headerId));
    }

    protected override Task<List<InvoiceDetailEntity>> GetFromEntity(IEnumerable<int> ids)
    {
        return _invoiceDetailDbEntity.Get(ids);
    }

    protected override int GetId(InvoiceDetailEntity obj)
    {
        return obj.InvoiceDetailId;
    }
}
