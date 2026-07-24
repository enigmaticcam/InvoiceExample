using Invoice_Logic.Core.Interfaces;
using Invoice_Logic.Core.Objects.ResultStatusTypeActions;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Factories;
using Invoice_Logic.Repositories.CacheEntities;

namespace Invoice_Logic.Core.Objects;

public class ResultStatusTypeCore : IResultStatusTypeCore
{
    private IFactoryMain _factory;
    private IResultStatusTypeCacheEntity _resultStatusTypeCacheEntity;

    public ResultStatusTypeCore(IFactoryMain factory, IResultStatusTypeCacheEntity resultStatusTypeCacheEntity)
    {
        _factory = factory;
        _resultStatusTypeCacheEntity = resultStatusTypeCacheEntity;
    }

    public Task<List<ResultStatusTypeEntity>> Get()
    {
        var action = new ActionGet(_resultStatusTypeCacheEntity, _factory.Repository);
        return action.Perform();
    }
}
