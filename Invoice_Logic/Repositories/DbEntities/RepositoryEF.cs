using Invoice_Logic.Caching;
using Invoice_Logic.Data.EF;

namespace Invoice_Logic.Repositories.DbEntities;

public class RepositoryEF : IRepository
{
    private Invoice_Context _context;
    private ICache _cache;
    private ILateLoaderCollection _lateLoaders;
    private IAllItemCollections _allItemCollections;

    public RepositoryEF(Invoice_Context context, ICache cache, ILateLoaderCollection lateLoaders, IAllItemCollections allItemCollections)
    {
        _context = context;
        _cache = cache;
        _lateLoaders = lateLoaders;
        _allItemCollections = allItemCollections;
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
        await _lateLoaders.Commit();
        await _cache.CommitQueue();
        _allItemCollections.ClearAll();
    }
}
