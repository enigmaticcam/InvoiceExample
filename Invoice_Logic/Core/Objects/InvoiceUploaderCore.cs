using Invoice_Logic.Core.Interfaces;
using Invoice_Logic.Core.Objects.InvoiceUploaderActions;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Factories;

namespace Invoice_Logic.Core.Objects;

public interface IInvoiceUploaderCoreActions
{
    Task Set(List<InvoiceHeaderEntity> invoices);
}

public class InvoiceUploaderCore : IInvoiceUploaderCore, IInvoiceUploaderCoreActions
{
    private IFactoryMain _factory;
    private string UploaderKey => "InvoiceUploader";

    public InvoiceUploaderCore(IFactoryMain factory)
    {
        _factory = factory;
    }

    public Task<List<InvoiceHeaderEntity>> Create(Stream stream)
    {
        var action = new ActionLoad(
            excel: _factory.Excel,
            webServer: _factory.WebServer,
            userLogging: _factory.UserLogging,
            invoiceHeaderCore: _factory.InvoiceHeaderCore,
            repository: _factory.Repository,
            actions: this
        );
        return action.Perform(stream);
    }

    public async Task<List<InvoiceHeaderEntity>> Get()
    {
        var result = await _factory.Cache.GetOrCreate(UploaderKey, () => Task.FromResult(new List<InvoiceHeaderEntity>()));
        if (result == null)
        {
            return new();
        }
        return result;
    }

    public async Task Set(List<InvoiceHeaderEntity> invoices)
    {
        await _factory.Cache.Set(UploaderKey, invoices);
    }

    public Task<string> GetRandom()
    {
        var action = new ActionGetRandom(_factory.ConnectionControl);
        return action.Perform();
    }
}
