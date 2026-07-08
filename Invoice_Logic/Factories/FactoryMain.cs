using Invoice_Logic.API;
using Invoice_Logic.API.Pipelines;
using Invoice_Logic.Caching;
using Invoice_Logic.Caching.InMemory;
using Invoice_Logic.Core.Interfaces;
using Invoice_Logic.Core.Objects;
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
    ICache Cache { get; }
    IInvoiceHeaderCore InvoiceHeaderCore { get; }
    IInvoiceSearchCore InvoiceSearchCore { get; }
    IPipeline Pipeline { get; }
}

public class FactoryMain : IFactoryMain
{
    private Lazy<IAllItemCollections> _allItemCollections;
    private Lazy<ICache> _cache;
    private Lazy<IInvoiceHeaderCacheEntity> _invoiceHeaderCacheEntity;
    private Lazy<IInvoiceHeaderCore> _invoiceHeaderCore;
    private Lazy<IInvoiceHeaderDbEntity> _invoiceHeaderDbEntity;
    private Lazy<IInvoiceHeaderCollection> _invoiceHeaderCollection;
    private Lazy<IInvoiceDetailCacheEntity> _invoiceDetailCacheEntity;
    private Lazy<IInvoiceDetailDbEntity> _invoiceDetailDbEntity;
    private Lazy<IInvoiceSearchCore> _invoiceSearchCore;
    private Lazy<ILateLoaderCollection> _lateLoaderCollection;
    private Lazy<IPipeline> _pipeline;
    private Lazy<IUserLogging> _userLogging;

    public FactoryMain(Invoice_Context context, IMemoryCache memoryCache, CacheOptions cacheOptions)
    {
        _allItemCollections = new Lazy<IAllItemCollections>(() => new AllItemCollections());
        _cache = new Lazy<ICache>(() => new CacheInMemory(cacheOptions, memoryCache));
        _invoiceDetailCacheEntity = new Lazy<IInvoiceDetailCacheEntity>(() => new InvoiceDetailCacheEntity(Cache, InvoiceDetailDbEntity));
        _invoiceDetailDbEntity = new Lazy<IInvoiceDetailDbEntity>(() => new InvoiceDetailDbEntity(context, InvoiceHeaderCollection, UserLogging));
        _invoiceHeaderCacheEntity = new Lazy<IInvoiceHeaderCacheEntity>(() => new InvoiceHeaderCacheEntity(Cache, InvoiceHeaderDbEntity));
        _invoiceHeaderCore = new Lazy<IInvoiceHeaderCore>(() => new InvoiceHeaderCore(this, InvoiceHeaderCacheEntity, InvoiceDetailCacheEntity));
        _invoiceHeaderDbEntity = new Lazy<IInvoiceHeaderDbEntity>(() => new InvoiceHeaderDbEntity(context, LateLoaderCollection, InvoiceHeaderCollection, UserLogging));
        _invoiceHeaderCollection = new Lazy<IInvoiceHeaderCollection>(() => new InvoiceHeaderCollection(AllItemCollections));
        _invoiceSearchCore = new Lazy<IInvoiceSearchCore>(() => new InvoiceSearchCore(this));
        _lateLoaderCollection = new Lazy<ILateLoaderCollection>(() => new LateLoaderCollection());
        _pipeline = new Lazy<IPipeline>(CreatePipeline);
        _userLogging = new Lazy<IUserLogging>(() => new UserLogging());
    }

    private IPipeline CreatePipeline()
    {
        IPipeline pipeline = new Pipeline();
        pipeline = new ExceptionPipeline(pipeline);
        pipeline = new UserLoggingPipeline(pipeline, UserLogging);
        return pipeline;
    }

    public IAllItemCollections AllItemCollections => _allItemCollections.Value;
    public ICache Cache => _cache.Value;
    public IInvoiceHeaderCore InvoiceHeaderCore => _invoiceHeaderCore.Value;
    public IInvoiceDetailCacheEntity InvoiceDetailCacheEntity => _invoiceDetailCacheEntity.Value;
    public IInvoiceDetailDbEntity InvoiceDetailDbEntity => _invoiceDetailDbEntity.Value;
    public IInvoiceHeaderCacheEntity InvoiceHeaderCacheEntity => _invoiceHeaderCacheEntity.Value;
    public IInvoiceHeaderDbEntity InvoiceHeaderDbEntity => _invoiceHeaderDbEntity.Value;
    public IInvoiceHeaderCollection InvoiceHeaderCollection => _invoiceHeaderCollection.Value;
    public IInvoiceSearchCore InvoiceSearchCore => _invoiceSearchCore.Value;
    public ILateLoaderCollection LateLoaderCollection => _lateLoaderCollection.Value;
    public IPipeline Pipeline => _pipeline.Value;
    public IUserLogging UserLogging => _userLogging.Value;
}
