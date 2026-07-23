using Invoice_Logic.Caching;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Repositories.DbEntities.Interfaces;

namespace Invoice_Logic.Repositories.CacheEntities;

public interface IInvoiceResultCacheEntity
{
    Task Delete(int headerId);
    Task<List<InvoiceResultEntity>> Get(int headerId);
}

public class InvoiceResultCacheEntity : CacheEntity<int, InvoiceResultEntity>, IInvoiceResultCacheEntity
{
    private IInvoiceResultDbEntity _invoiceResultDbEntity;
    public InvoiceResultCacheEntity(ICache cache, IInvoiceResultDbEntity invoiceResultDbEntity) : base(cache)
    {
        _invoiceResultDbEntity = invoiceResultDbEntity;
    }

    protected override string ObjectKey => "InvoiceResultObject";
    private string ListKey_ByHeader(int headerId) => $"InvoiceResultList_ByHeader_{headerId}";

    public Task<List<InvoiceResultEntity>> Get(int headerId)
    {
        return GetFromCache(ListKey_ByHeader(headerId), () => _invoiceResultDbEntity.Get(headerId));
    }

    protected override Task<List<InvoiceResultEntity>> GetFromEntity(IEnumerable<int> ids)
    {
        return _invoiceResultDbEntity.Get(ids);
    }

    protected override int GetId(InvoiceResultEntity obj)
    {
        return obj.InvoiceResultId;
    }

    public async Task Delete(int headerId)
    {
        var result = await _invoiceResultDbEntity.Delete(headerId);
        CacheQueueClear(result);
        CacheQueueClearList(ListKey_ByHeader(headerId));
    }
}
