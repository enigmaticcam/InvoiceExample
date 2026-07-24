using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Enums;
using Invoice_Logic.Repositories;
using Invoice_Logic.Repositories.CacheEntities;

namespace Invoice_Logic.Core.Objects.ResultStatusTypeActions;

public class ActionGet
{
    private IResultStatusTypeCacheEntity _resultStatusTypeCacheEntity;
    private IRepository _repository;

    public ActionGet(IResultStatusTypeCacheEntity resultStatusTypeCacheEntity, IRepository repository)
    {
        _resultStatusTypeCacheEntity = resultStatusTypeCacheEntity;
        _repository = repository;
    }

    public async Task<List<ResultStatusTypeEntity>> Perform()
    {
        var items = await _resultStatusTypeCacheEntity.Get();
        if (items.Count == 0)
        {
            items = await CreateDefaults();
        }
        return items;
    }

    private async Task<List<ResultStatusTypeEntity>> CreateDefaults()
    {
        var creates = new List<ResultStatusTypeCreateDTO>()
        {
            new ResultStatusTypeCreateDTO(
                ResultStatusTypeId: (int)enumnResultStatusType.New,
                ResultStatusTypeDesc: "New"
            ),
            new ResultStatusTypeCreateDTO(
                ResultStatusTypeId: (int)enumnResultStatusType.InvalidRate,
                ResultStatusTypeDesc: "Invalid Rate"
            ),
            new ResultStatusTypeCreateDTO(
                ResultStatusTypeId: (int)enumnResultStatusType.ItemCodeLookupFail,
                ResultStatusTypeDesc: "Item Code Lookup Fail"
            ),
            new ResultStatusTypeCreateDTO(
                ResultStatusTypeId: (int)enumnResultStatusType.Pass,
                ResultStatusTypeDesc: "Pass"
            )
        };
        var result = await _resultStatusTypeCacheEntity.Create(creates);
        await _repository.SaveChanges();
        return result.LoadObject!;
    }
}
