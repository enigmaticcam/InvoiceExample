using Invoice_Logic.Caching;
using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Repositories.DbEntities.Interfaces;

namespace Invoice_Logic.Repositories.CacheEntities;

public interface IInvoiceHeaderCacheEntity
{
    Task<LateLoader<int, InvoiceHeaderEntity>> Create(InvoiceHeaderCreateDTO create);
    Task<InvoiceHeaderEntity> Get(int id);
    Task<List<InvoiceHeaderEntity>> Get(InvoiceFilterDTO filter);
}

public class InvoiceHeaderCacheEntity : CacheEntity<int, InvoiceHeaderEntity>, IInvoiceHeaderCacheEntity
{
    private IInvoiceHeaderDbEntity _invoiceHeaderDbEntity;
    public InvoiceHeaderCacheEntity(ICache cache, IInvoiceHeaderDbEntity invoiceHeaderDbEntity) : base(cache)
    {
        _invoiceHeaderDbEntity = invoiceHeaderDbEntity;
    }

    protected override string ObjectKey => "InvoiceHeaderObject";

    public async Task<LateLoader<int, InvoiceHeaderEntity>> Create(InvoiceHeaderCreateDTO create)
    {
        var result = await _invoiceHeaderDbEntity.Create(create);
        CacheQueueSet(() => result.LoadObject!);
        return result;
    }

    public Task<InvoiceHeaderEntity> Get(int id)
    {
        return GetFromCache(id);
    }

    public async Task<List<InvoiceHeaderEntity>> Get(InvoiceFilterDTO filter)
    {
        var result = await _invoiceHeaderDbEntity.Get(filter);
        return await GetFromCache(result);
    }

    protected override Task<List<InvoiceHeaderEntity>> GetFromEntity(IEnumerable<int> ids)
    {
        return _invoiceHeaderDbEntity.Get(ids);
    }

    protected override int GetId(InvoiceHeaderEntity obj)
    {
        return obj.InvoiceHeaderId;
    }
}
