using Invoice_Logic.Core.Interfaces;
using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Factories;

namespace Invoice_Logic.Core.Objects;

public class InvoiceSearchCore : IInvoiceSearchCore
{
    private IFactoryMain _factory;

    public InvoiceSearchCore(IFactoryMain factory)
    {
        _factory = factory;
    }

    private string SearchKey => "InvoiceSearch";

    public async Task<InvoiceSearchDTO> Get()
    {
        var search = await _factory.Cache.GetOrCreate(SearchKey, () =>
        {
            var result = new InvoiceSearchDTO();
            return Task.FromResult(result);
        });
        if (search == null)
        {
            throw new NullReferenceException("Cache returned null result for BB Search");
        }
        return search;
    }

    public async Task<InvoiceSearchDTO> Get(InvoiceFilterDTO filter)
    {
        var invoices = await _factory.InvoiceHeaderCore.Get(filter);
        var search = new InvoiceSearchDTO(filter, invoices);
        await _factory.Cache.Set(SearchKey, search);
        return search;
    }
}
