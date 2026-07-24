using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;

namespace Invoice_Logic.Repositories.DbEntities.Interfaces;

public interface IStatusTypeDbEntity
{
    Task<LateLoader<List<StatusTypeEntity>>> Create(IEnumerable<StatusTypeCreateDTO> creates);
    Task<List<int>> Get();
    Task<List<StatusTypeEntity>> Get(IEnumerable<int> ids);
}
