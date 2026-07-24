using Invoice_Logic.Core.Interfaces;
using Invoice_Logic.Core.Objects.StatusTypeActions;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Factories;
using Invoice_Logic.Repositories.CacheEntities;

namespace Invoice_Logic.Core.Objects;

public class StatusTypeCore : IStatusTypeCore
{
    private IFactoryMain _factory;
    private IStatusTypeCacheEntity _statusTypeCacheEntity;

    public StatusTypeCore(IFactoryMain factory, IStatusTypeCacheEntity statusTypeCacheEntity)
    {
        _factory = factory;
        _statusTypeCacheEntity = statusTypeCacheEntity;
    }

    public Task<List<StatusTypeEntity>> Get()
    {
        var action = new ActionGet(_statusTypeCacheEntity, _factory.Repository);
        return action.Perfoim();
    }
}
