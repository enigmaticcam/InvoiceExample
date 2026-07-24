using Invoice_Logic.Caching;
using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Repositories.DbEntities.Interfaces;

namespace Invoice_Logic.Repositories.CacheEntities;

public interface IResultStatusTypeCacheEntity
{
    Task<LateLoader<List<ResultStatusTypeEntity>>> Create(IEnumerable<ResultStatusTypeCreateDTO> creates);
    Task<List<ResultStatusTypeEntity>> Get();
}

public class ResultStatusTypeCacheEntity : CacheEntity<int, ResultStatusTypeEntity>, IResultStatusTypeCacheEntity
{
    private IResultStatusTypeDbEntity _resultStatusTypeDbEntity;
    public ResultStatusTypeCacheEntity(ICache cache, IResultStatusTypeDbEntity resultStatusTypeDbEntity) : base(cache)
    {
        _resultStatusTypeDbEntity = resultStatusTypeDbEntity;
    }

    protected override string ObjectKey => "ResultStatusTypeObject";
    private string ListKey_All => "ResultStatusTypeList_All";

    public async Task<LateLoader<List<ResultStatusTypeEntity>>> Create(IEnumerable<ResultStatusTypeCreateDTO> creates)
    {
        var result = await _resultStatusTypeDbEntity.Create(creates);
        CacheQueueSet(() => result.LoadObject!);
        await CacheQueueClearList();
        return result;
    }

    public Task<List<ResultStatusTypeEntity>> Get()
    {
        return GetFromCache(ListKey_All, _resultStatusTypeDbEntity.Get);
    }

    protected override Task<List<ResultStatusTypeEntity>> GetFromEntity(IEnumerable<int> ids)
    {
        return _resultStatusTypeDbEntity.Get(ids);
    }

    protected override int GetId(ResultStatusTypeEntity obj)
    {
        return obj.ResultStatusTypeId;
    }
}
