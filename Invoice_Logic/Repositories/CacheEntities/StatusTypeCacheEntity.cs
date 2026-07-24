using Invoice_Logic.Caching;
using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Repositories.DbEntities.Interfaces;

namespace Invoice_Logic.Repositories.CacheEntities;

public interface IStatusTypeCacheEntity
{
    Task<LateLoader<List<StatusTypeEntity>>> Create(IEnumerable<StatusTypeCreateDTO> creates);
    Task<List<StatusTypeEntity>> Get();
}

public class StatusTypeCacheEntity : CacheEntity<int, StatusTypeEntity>, IStatusTypeCacheEntity
{
    private IStatusTypeDbEntity _statusTypeDbEntity;
    public StatusTypeCacheEntity(ICache cache, IStatusTypeDbEntity statusTypeDbEntity) : base(cache)
    {
        _statusTypeDbEntity = statusTypeDbEntity;
    }

    protected override string ObjectKey => "StatusTypeObject";
    private string ListKey_All => "StatusTypeList_All";

    public async Task<LateLoader<List<StatusTypeEntity>>> Create(IEnumerable<StatusTypeCreateDTO> creates)
    {
        var result = await _statusTypeDbEntity.Create(creates);
        CacheQueueSet(() => result.LoadObject!);
        await CacheQueueClearList();
        return result;
    }

    public Task<List<StatusTypeEntity>> Get()
    {
        return GetFromCache(ListKey_All, _statusTypeDbEntity.Get);
    }

    protected override Task<List<StatusTypeEntity>> GetFromEntity(IEnumerable<int> ids)
    {
        return _statusTypeDbEntity.Get(ids);
    }

    protected override int GetId(StatusTypeEntity obj)
    {
        return obj.StatusTypeId;
    }
}
