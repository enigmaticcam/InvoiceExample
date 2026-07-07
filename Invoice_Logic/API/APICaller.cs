using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Factories;

namespace Invoice_Logic.API;

public interface IAPICaller
{
    Task<APIResult<InvoiceHeaderEntity>> InvoiceHeader_Get(int id);
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
}
