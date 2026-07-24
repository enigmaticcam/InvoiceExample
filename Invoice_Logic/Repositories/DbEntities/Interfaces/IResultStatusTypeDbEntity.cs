using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;

namespace Invoice_Logic.Repositories.DbEntities.Interfaces;

public interface IResultStatusTypeDbEntity
{
    Task<LateLoader<List<ResultStatusTypeEntity>>> Create(IEnumerable<ResultStatusTypeCreateDTO> creates);
    Task<List<int>> Get();
    Task<List<ResultStatusTypeEntity>> Get(IEnumerable<int> ids);
}
