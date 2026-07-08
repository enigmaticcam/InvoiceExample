using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Factories;

namespace Invoice_Logic.API;

public interface IAPICaller
{
    Task<APIResult<InvoiceHeaderEntity>> InvoiceHeader_Get(int id);
    Task<APIResult<InvoiceSearchDTO>> InvoiceSearch_Get();
    Task<APIResult<InvoiceSearchDTO>> InvoiceSearch_Get(InvoiceFilterDTO filter);
}

public class APICaller : IAPICaller
{
    private IFactoryMain _factory;

    public APICaller(IFactoryMain factory)
    {
        _factory = factory;
    }

    public Task<APIResult<InvoiceHeaderEntity>> InvoiceHeader_Get(int id)
    {
        return _factory.Pipeline.Perform(
            action: () => _factory.InvoiceHeaderCore.Get(id),
            actionName: "InvoiceHeader_Get");
    }

    public Task<APIResult<InvoiceSearchDTO>> InvoiceSearch_Get()
    {
        return _factory.Pipeline.Perform(
            action: () => _factory.InvoiceSearchCore.Get(),
            actionName: "InvoiceSearch_Get");
    }

    public Task<APIResult<InvoiceSearchDTO>> InvoiceSearch_Get(InvoiceFilterDTO filter)
    {
        return _factory.Pipeline.Perform(
            action: () => _factory.InvoiceSearchCore.Get(filter),
            actionName: "InvoiceSearch_GetWithFilter");
    }
}
