using Invoice_Logic.API;
using Invoice_Logic.Caching;
using Invoice_Logic.Caching.InMemory;
using Invoice_Logic.Data.EF;
using Invoice_Logic.Repositories;
using Invoice_Logic.Repositories.CacheEntities;
using Invoice_Logic.Repositories.DbEntities.Interfaces;
using Invoice_Logic.Repositories.DbEntities.Objects;
using Invoice_Logic.Repositories.ItemCollections;
using Microsoft.Extensions.Caching.Memory;

namespace Invoice_Logic.Factories;

public interface IFactoryMain
{
}

public class FactoryMain : IFactoryMain
{
    private Lazy<IAllItemCollections> _allItemCollections;
    private Lazy<ICache> _cache;
    private Lazy<IInvoiceHeaderCacheEntity> _invoiceHeaderCacheEntity;
    private Lazy<IInvoiceHeaderDbEntity> _invoiceHeaderDbEntity;
    private Lazy<IInvoiceHeaderCollection> _invoiceHeaderCollection;
    private Lazy<IInvoiceDetailCacheEntity> _invoiceDetailCacheEntity;
    private Lazy<IInvoiceDetailDbEntity> _invoiceDetailDbEntity;
    private Lazy<ILateLoaderCollection> _lateLoaderCollection;
    private Lazy<IUserLogging> _userLogging;

    public FactoryMain(Invoice_Context context, IMemoryCache memoryCache, CacheOptions cacheOptions)
    {
        _allItemCollections = new Lazy<IAllItemCollections>(() => new AllItemCollections());
        _cache = new Lazy<ICache>(() => new CacheInMemory(cacheOptions, memoryCache));
        _invoiceDetailCacheEntity = new Lazy<IInvoiceDetailCacheEntity>(() => new InvoiceDetailCacheEntity(Cache, InvoiceDetailDbEntity));
        _invoiceDetailDbEntity = new Lazy<IInvoiceDetailDbEntity>(() => new InvoiceDetailDbEntity(context, InvoiceHeaderCollection, UserLogging));
        _invoiceHeaderCacheEntity = new Lazy<IInvoiceHeaderCacheEntity>(() => new InvoiceHeaderCacheEntity(Cache, InvoiceHeaderDbEntity));
        _invoiceHeaderDbEntity = new Lazy<IInvoiceHeaderDbEntity>(() => new InvoiceHeaderDbEntity(context, LateLoaderCollection, InvoiceHeaderCollection, UserLogging));
        _invoiceHeaderCollection = new Lazy<IInvoiceHeaderCollection>(() => new InvoiceHeaderCollection(AllItemCollections));
        _lateLoaderCollection = new Lazy<ILateLoaderCollection>(() => new LateLoaderCollection());
        _userLogging = new Lazy<IUserLogging>(() => new UserLogging());
    }
    public IAllItemCollections AllItemCollections => _allItemCollections.Value;
    public ICache Cache => _cache.Value;
    public IInvoiceDetailCacheEntity InvoiceDetailCacheEntity => _invoiceDetailCacheEntity.Value;
    public IInvoiceDetailDbEntity InvoiceDetailDbEntity => _invoiceDetailDbEntity.Value;
    public IInvoiceHeaderCacheEntity InvoiceHeaderCacheEntity => _invoiceHeaderCacheEntity.Value;
    public IInvoiceHeaderDbEntity InvoiceHeaderDbEntity => _invoiceHeaderDbEntity.Value;
    public IInvoiceHeaderCollection InvoiceHeaderCollection => _invoiceHeaderCollection.Value;
    public ILateLoaderCollection LateLoaderCollection => _lateLoaderCollection.Value;
    public IUserLogging UserLogging => _userLogging.Value;
}
