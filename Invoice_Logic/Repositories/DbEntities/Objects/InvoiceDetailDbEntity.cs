using Invoice_Logic.API;
using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Data.EF;
using Invoice_Logic.Repositories.DbEntities.Interfaces;
using Invoice_Logic.Repositories.ItemCollections;
using Microsoft.EntityFrameworkCore;

namespace Invoice_Logic.Repositories.DbEntities.Objects;

public class InvoiceDetailDbEntity : IInvoiceDetailDbEntity
{
    private Invoice_Context _context;
    private IInvoiceHeaderCollection _invoiceHeaderCollection;
    private IUserLogging _userLogging;

    public InvoiceDetailDbEntity(Invoice_Context context, IInvoiceHeaderCollection invoiceHeaderCollection, IUserLogging userLogging)
    {
        _context = context;
        _invoiceHeaderCollection = invoiceHeaderCollection;
        _userLogging = userLogging;
    }

    public async Task Create(int headerId, IEnumerable<InvoiceDetailCreateDTO> creates)
    {
        var header = _invoiceHeaderCollection.GetItem(headerId);
        var details = creates
            .Select(Mapper.ToEf)
            .ToList();
        details.ForEach(x => x.InvoiceHeader = header);
        await _context.AddRangeAsync(details);
    }

    public async Task<List<int>> Delete(int headerId)
    {
        var lines = await _context.InvoiceDetails
            .Where(x => x.InvoiceHeaderId == headerId)
            .ToListAsync();
        _context.RemoveRange(lines);
        return lines
            .Select(x => x.InvoiceDetailId)
            .ToList();
    }

    public Task<List<int>> Get(int headerId)
    {
        return _context.InvoiceDetails
            .Where(x => x.InvoiceHeaderId == headerId)
            .Select(x => x.InvoiceDetailId)
            .ToListAsync();
    }

    public async Task<List<InvoiceDetailEntity>> Get(IEnumerable<int> ids)
    {
        var result = await GetFromDb(ids);
        return result
            .Select(Mapper.FromEf)
            .ToList();
    }

    private async Task<List<InvoiceDetail>> GetFromDb(IEnumerable<int> ids)
    {
        var result = await _context.InvoiceDetails
            .Where(x => ids.Contains(x.InvoiceDetailId))
            .ToListAsync();
        var diff = ids.Except(result.Select(x => x.InvoiceDetailId));
        if (diff.Count() > 0)
        {
            _userLogging.ThrowInvoiceDetailNotFoundException(diff);
        }
        return result;
    }
}
