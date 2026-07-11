using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Factories;

namespace Invoice_Logic.API;

public interface IAPICaller
{
    Task<APIResult<List<InvoiceDetailEntity>>> InvoiceDetail_Get(int headerId);
    Task<APIResult<InvoiceHeaderEntity>> InvoiceHeader_Get(int id);
    Task<APIResult<List<InvoiceDetailEntity>>> InvoiceHeader_RefreshResults(int id);
    Task<APIResult<InvoiceSearchDTO>> InvoiceSearch_Get();
    Task<APIResult<InvoiceSearchDTO>> InvoiceSearch_Get(InvoiceFilterDTO filter);
    Task<APIResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Get();
    Task<APIResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Create(Stream stream);

}

public class APICaller : IAPICaller
{
    private IFactoryMain _factory;

    public APICaller(IFactoryMain factory)
    {
        _factory = factory;
    }

    public Task<APIResult<List<InvoiceDetailEntity>>> InvoiceDetail_Get(int headerId)
    {
        return _factory.Pipeline.Perform(
            action: () => _factory.InvoiceHeaderCore.GetDetail(headerId),
            actionName: "InvoiceDetail_Get");
    }

    public Task<APIResult<InvoiceHeaderEntity>> InvoiceHeader_Get(int id)
    {
        return _factory.Pipeline.Perform(
            action: () => _factory.InvoiceHeaderCore.Get(id),
            actionName: "InvoiceHeader_Get");
    }

    public Task<APIResult<List<InvoiceDetailEntity>>> InvoiceHeader_RefreshResults(int id)
    {
        return _factory.Pipeline.Perform(
            action: () => _factory.InvoiceHeaderCore.UpdateRefreshResults(id),
            actionName: "InvoiceHeader_RefreshResults");
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

    public Task<APIResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Create(Stream stream)
    {
        return _factory.Pipeline.Perform(
            action: () => _factory.InvoiceUploaderCore.Create(stream),
            actionName: "InvoiceUploader_Create");
    }

    public Task<APIResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Get()
    {
        return _factory.Pipeline.Perform(
            action: () => _factory.InvoiceUploaderCore.Get(),
            actionName: "InvoiceUploader_Get");
    }
}
