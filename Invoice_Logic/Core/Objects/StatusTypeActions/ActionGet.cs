using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Enums;
using Invoice_Logic.Repositories;
using Invoice_Logic.Repositories.CacheEntities;

namespace Invoice_Logic.Core.Objects.StatusTypeActions;

public class ActionGet
{
    private IStatusTypeCacheEntity _statusTypeCacheEntity;
    private IRepository _respository;

    public ActionGet(IStatusTypeCacheEntity statusTypeCacheEntity, IRepository respository)
    {
        _statusTypeCacheEntity = statusTypeCacheEntity;
        _respository = respository;
    }

    public async Task<List<StatusTypeEntity>> Perfoim()
    {
        var items = await _statusTypeCacheEntity.Get();
        if (items.Count == 0)
        {
            items = await CreateDefaults();
        }
        return items;
    }

    private async Task<List<StatusTypeEntity>> CreateDefaults()
    {
        var creates = new List<StatusTypeCreateDTO>()
        {
            new StatusTypeCreateDTO(
                StatusTypeId: (int)enumStatusType.Approved,
                StatusTypeDesc: "Approved"
            ),
            new StatusTypeCreateDTO(
                StatusTypeId: (int)enumStatusType.Draft,
                StatusTypeDesc: "Draft"
            ),
            new StatusTypeCreateDTO(
                StatusTypeId: (int)enumStatusType.Finished,
                StatusTypeDesc: "Finished"
            )
        };
        var result = await _statusTypeCacheEntity.Create(creates);
        await _respository.SaveChanges();
        return result.LoadObject!;
    }
}
