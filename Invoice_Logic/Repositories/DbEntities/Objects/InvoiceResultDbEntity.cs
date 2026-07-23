using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Data.EF;
using Invoice_Logic.Repositories.DbEntities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoice_Logic.Repositories.DbEntities.Objects;

public class InvoiceResultDbEntity : IInvoiceResultDbEntity
{
    private Invoice_Context _context;

    public InvoiceResultDbEntity(Invoice_Context context)
    {
        _context = context;
    }

    public async Task<List<int>> Delete(int headerId)
    {
        var lines = await _context.InvoiceResults
            .Where(x => x.InvoiceHeaderId == headerId)
            .ToListAsync();
        _context.RemoveRange(lines);
        return lines
            .Select(x => x.InvoiceResultId)
            .ToList();
    }

    public Task<List<int>> Get(int headerId)
    {
        return _context.InvoiceResults
            .Where(x => x.InvoiceHeaderId == headerId)
            .Select(x => x.InvoiceResultId)
            .ToListAsync();
    }

    public async Task<List<InvoiceResultEntity>> Get(IEnumerable<int> ids)
    {
        var result = await _context.InvoiceResults
            .Where(x => ids.Contains(x.InvoiceResultId))
            .ToListAsync();
        return Mapper.FromEf(result);
    }
}
