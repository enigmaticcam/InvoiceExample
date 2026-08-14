using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Data.DTOs.Update;
using Invoice_Logic.Factories;

namespace Invoice_Logic.API;

public interface IAPICaller
{
    Task<APIResult> InvoiceHeader_Delete(int id);
    Task<APIResult<InvoiceHeaderEntity>> InvoiceHeader_Get(int id);
    Task<APIResult<InvoicePermissionsDTO>> InvoiceHeader_GetPermissions(int id);
    Task<APIResult<List<InvoiceFullResultDTO>>> InvoiceHeader_GetResults(int id);
    Task<APIResult<List<InvoiceFullResultDTO>>> InvoiceHeader_RefreshResults(int id);
    Task<APIResult<InvoiceUpdateResultDTO>> InvoiceHeader_Update(int id, int statusTypeId);
    Task<APIResult<List<InvoiceFullResultDTO>>> InvoiceHeader_Update(int id, IEnumerable<InvoiceDetailUpdateDTO> updates);
    Task<APIResult<InvoiceSearchDTO>> InvoiceSearch_Get();
    Task<APIResult<InvoiceSearchDTO>> InvoiceSearch_Get(InvoiceFilterDTO filter);
    Task<APIResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Get();
    Task<APIResult<string>> InvoiceUploader_GetBlankTemplate();
    Task<APIResult<RandomInvoiceDTO>> InvoiceUploader_GetRandom();
    Task<APIResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Create(Stream stream);
    Task<APIResult<List<ResultStatusTypeEntity>>> ResultStatusType_Get();
    Task<APIResult<List<StatusTypeEntity>>> StatusType_Get();

}

public class APICaller : IAPICaller
{
    private IFactoryMain _factory;

    public APICaller(IFactoryMain factory)
    {
        _factory = factory;
    }

    public Task<APIResult> InvoiceHeader_Delete(int id)
    {
        return _factory.Pipeline.Perform(
            action: () => _factory.InvoiceHeaderCore.Delete(id),
            actionName: "InvoiceHeader_Delete");
    }

    public Task<APIResult<InvoiceHeaderEntity>> InvoiceHeader_Get(int id)
    {
        return _factory.Pipeline.Perform(
            action: () => _factory.InvoiceHeaderCore.Get(id),
            actionName: "InvoiceHeader_Get");
    }

    public Task<APIResult<InvoicePermissionsDTO>> InvoiceHeader_GetPermissions(int id)
    {
        return _factory.Pipeline.Perform(
            action: () => _factory.InvoiceHeaderCore.GetPermissions(id),
            actionName: "InvoiceHeader_GetPermissions");
    }

    public Task<APIResult<List<InvoiceFullResultDTO>>> InvoiceHeader_GetResults(int id)
    {
        return _factory.Pipeline.Perform(
            action: () => _factory.InvoiceHeaderCore.GetResults(id),
            actionName: "InvoiceHeader_GetFull");
    }

    public Task<APIResult<List<InvoiceFullResultDTO>>> InvoiceHeader_RefreshResults(int id)
    {
        return _factory.Pipeline.Perform(
            action: () => _factory.InvoiceHeaderCore.UpdateRefreshResults(id),
            actionName: "InvoiceHeader_RefreshResults");
    }

    public Task<APIResult<InvoiceUpdateResultDTO>> InvoiceHeader_Update(int id, int statusTypeId)
    {
        return _factory.Pipeline.Perform(
            action: () => _factory.InvoiceHeaderCore.Update(id, statusTypeId),
            actionName: "InvoiceHeader_Update");
    }

    public Task<APIResult<List<InvoiceFullResultDTO>>> InvoiceHeader_Update(int id, IEnumerable<InvoiceDetailUpdateDTO> updates)
    {
        return _factory.Pipeline.Perform(
            action: () => _factory.InvoiceHeaderCore.Update(id, updates),
            actionName: "InvoiceHeader_Update");
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

    public Task<APIResult<string>> InvoiceUploader_GetBlankTemplate()
    {
        return _factory.Pipeline.Perform(
            action: () => Task.FromResult(_factory.InvoiceUploaderCore.GetBlankTemplate()),
            actionName: "InvoiceUploader_GetBlankTemplate");
    }

    public Task<APIResult<RandomInvoiceDTO>> InvoiceUploader_GetRandom()
    {
        return _factory.Pipeline.Perform(
            action: () => _factory.InvoiceUploaderCore.GetRandom(),
            actionName: "InvoiceUploader_GetRandom");
    }

    public Task<APIResult<List<ResultStatusTypeEntity>>> ResultStatusType_Get()
    {
        return _factory.Pipeline.Perform(
            action: () => _factory.ResultStatusTypeCore.Get(),
            actionName: "ResultStatusType_Get");
    }

    public Task<APIResult<List<StatusTypeEntity>>> StatusType_Get()
    {
        return _factory.Pipeline.Perform(
            action: () => _factory.StatusTypeCore.Get(),
            actionName: "StatusType_Get");
    }
}
