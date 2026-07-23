using Invoice_Logic.API;
using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Data.EF;
using Invoice_Logic.Repositories.DbEntities.Interfaces;
using Invoice_Logic.Repositories.ItemCollections;
using Microsoft.EntityFrameworkCore;

namespace Invoice_Logic.Repositories.DbEntities.Objects;

public class InvoiceHeaderDbEntity : IInvoiceHeaderDbEntity
{
    private Invoice_Context _context;
    private ILateLoaderCollection _lateLoaderCollection;
    private IInvoiceHeaderCollection _invoiceHeaderCollection;
    private IUserLogging _userLogging;

    public InvoiceHeaderDbEntity(Invoice_Context context, ILateLoaderCollection lateLoaderCollection, IInvoiceHeaderCollection invoiceHeaderCollection, IUserLogging userLogging)
    {
        _context = context;
        _lateLoaderCollection = lateLoaderCollection;
        _invoiceHeaderCollection = invoiceHeaderCollection;
        _userLogging = userLogging;
    }

    public async Task<LateLoader<int, InvoiceHeaderEntity>> Create(InvoiceHeaderCreateDTO create)
    {
        var invoice = Mapper.ToEf(create);
        await _context.AddAsync(invoice);
        var tempId = _invoiceHeaderCollection.AddItem(invoice);
        return _lateLoaderCollection.Add(() => Task.FromResult(Mapper.FromEf(invoice)), tempId);
    }

    public async Task Delete(int id)
    {
        var header = await GetFromDb(new List<int>() { id });
        _context.Remove(header.First());
    }

    public async Task<List<InvoiceHeaderEntity>> Get(IEnumerable<int> ids)
    {
        var result = await GetFromDb(ids);
        return result
            .Select(Mapper.FromEf)
            .ToList();
    }

    public async Task<List<int>> Get(InvoiceFilterDTO filter)
    {
        var list = await _context.InvoiceHeaders
            .Where(x => (!filter.ByHeader || x.InvoiceHeaderId == filter.HeaderId)
                && (!filter.ByCustomer || x.Customer == filter.Customer)
                && (!filter.ByMonth || x.InvoiceDate.Year * 100 + x.InvoiceDate.Month == filter.MonthId))
            .OrderByDescending(x => x.InvoiceHeaderId)
            .Select(x => x.InvoiceHeaderId)
            .Take(100)
            .ToListAsync();
        return list;
    }

    private async Task<List<InvoiceHeader>> GetFromDb(IEnumerable<int> ids)
    {
        var result = await _context.InvoiceHeaders
            .Where(x => ids.Contains(x.InvoiceHeaderId))
            .ToListAsync();
        var diff = ids.Except(result.Select(x => x.InvoiceHeaderId));
        if (diff.Count() > 0)
        {
            _userLogging.ThrowInvoiceHeaderNotFoundException(diff);
        }
        return result;
    }
}
