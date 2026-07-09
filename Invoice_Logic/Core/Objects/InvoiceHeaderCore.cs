using Invoice_Logic.Core.Interfaces;
using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Factories;
using Invoice_Logic.Repositories;
using Invoice_Logic.Repositories.CacheEntities;

namespace Invoice_Logic.Core.Objects;

public class InvoiceHeaderCore : IInvoiceHeaderCore
{
    private IFactoryMain _factory;
    private IInvoiceHeaderCacheEntity _invoiceHeaderCacheEntity;
    private IInvoiceDetailCacheEntity _invoiceDetailCacheEntity;

    public InvoiceHeaderCore(IFactoryMain factory, IInvoiceHeaderCacheEntity invoiceHeaderCacheEntity, IInvoiceDetailCacheEntity invoiceDetailCacheEntity)
    {
        _factory = factory;
        _invoiceHeaderCacheEntity = invoiceHeaderCacheEntity;
        _invoiceDetailCacheEntity = invoiceDetailCacheEntity;
    }

    public Task<InvoiceHeaderEntity> Get(int id)
    {
        return _invoiceHeaderCacheEntity.Get(id);
    }

    public Task<List<InvoiceHeaderEntity>> Get(InvoiceFilterDTO filter)
    {
        return _invoiceHeaderCacheEntity.Get(filter);
    }

    public Task<List<InvoiceDetailEntity>> GetDetail(int id)
    {
        return _invoiceDetailCacheEntity.Get(id);
    }

    public Task<LateLoader<int, InvoiceHeaderEntity>> QueueCreate(InvoiceHeaderCreateDTO create)
    {
        return _invoiceHeaderCacheEntity.Create(create);
    }

    public Task QueueCreate(int headerId, IEnumerable<InvoiceDetailCreateDTO> creates)
    {
        return _invoiceDetailCacheEntity.Create(headerId, creates);
    }
}
