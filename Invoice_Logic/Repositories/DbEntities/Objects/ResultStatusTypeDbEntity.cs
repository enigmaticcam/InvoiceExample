using Invoice_Logic.API;
using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Data.EF;
using Invoice_Logic.Repositories.DbEntities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoice_Logic.Repositories.DbEntities.Objects;

public class ResultStatusTypeDbEntity : IResultStatusTypeDbEntity
{
    private Invoice_Context _context;
    private ILateLoaderCollection _lateLoaderCollection;
    private IUserLogging _userLogging;

    public ResultStatusTypeDbEntity(Invoice_Context context, ILateLoaderCollection lateLoaderCollection, IUserLogging userLogging)
    {
        _context = context;
        _lateLoaderCollection = lateLoaderCollection;
        _userLogging = userLogging;
    }

    public async Task<LateLoader<List<ResultStatusTypeEntity>>> Create(IEnumerable<ResultStatusTypeCreateDTO> creates)
    {
        var statuses = Mapper.ToEf(creates);
        await _context.AddRangeAsync(statuses);
        return _lateLoaderCollection.Add(() => Task.FromResult(Mapper.FromEf(statuses)));
    }

    public Task<List<int>> Get()
    {
        return _context.ResultStatusTypes
            .Select(x => x.ResultStatusTypeId)
            .ToListAsync();
    }

    public async Task<List<ResultStatusTypeEntity>> Get(IEnumerable<int> ids)
    {
        var result = await GetFromDb(ids);
        return Mapper.FromEf(result);
    }

    private async Task<List<ResultStatusType>> GetFromDb(IEnumerable<int> ids)
    {
        var result = await _context.ResultStatusTypes
            .Where(x => ids.Contains(x.ResultStatusTypeId))
            .ToListAsync();
        var diff = ids.Except(result.Select(x => x.ResultStatusTypeId));
        if (diff.Count() > 0)
        {
            _userLogging.ThrowResultStatusTypeNotFoundException(diff);
        }
        return result;
    }
}
