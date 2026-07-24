using Invoice_Logic.API;
using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Data.EF;
using Invoice_Logic.Repositories.DbEntities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoice_Logic.Repositories.DbEntities.Objects;

public class StatusTypeDbEntity : IStatusTypeDbEntity
{
    private Invoice_Context _context;
    private ILateLoaderCollection _lateLoaderCollection;
    private IUserLogging _userLogging;

    public StatusTypeDbEntity(Invoice_Context context, ILateLoaderCollection lateLoaderCollection, IUserLogging userLogging)
    {
        _context = context;
        _lateLoaderCollection = lateLoaderCollection;
        _userLogging = userLogging;
    }

    public async Task<LateLoader<List<StatusTypeEntity>>> Create(IEnumerable<StatusTypeCreateDTO> creates)
    {
        var statuses = Mapper.ToEf(creates);
        await _context.AddRangeAsync(statuses);
        return _lateLoaderCollection.Add(() => Task.FromResult(Mapper.FromEf(statuses)));
    }

    public Task<List<int>> Get()
    {
        return _context.StatusTypes
            .Select(x => x.StatusTypeId)
            .ToListAsync();
    }

    public async Task<List<StatusTypeEntity>> Get(IEnumerable<int> ids)
    {
        var result = await GetFromDb(ids);
        return Mapper.FromEf(result);
    }

    private async Task<List<StatusType>> GetFromDb(IEnumerable<int> ids)
    {
        var result = await _context.StatusTypes
            .Where(x => ids.Contains(x.StatusTypeId))
            .ToListAsync();
        var diff = ids.Except(result.Select(x => x.StatusTypeId));
        if (diff.Count() > 0)
        {
            _userLogging.ThrowStatusTypeNotFoundException(ids);
        }
        return result;
    }
}
