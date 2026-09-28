using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoice_Avalonia.Services.Core;

public interface IServiceWrapper
{
    Task<WebResult> InvoiceHeader_Delete(int headerId);
    Task<WebResult<InvoiceHeaderEntity>> InvoiceHeader_Get(int headerId);
    Task<WebResult<InvoicePermissionsDTO>> InvoiceHeader_GetPermissions(int headerId);
    Task<WebResult<List<InvoiceFullResultDTO>>> InvoiceHeader_GetResults(int headerId);
    Task<WebResult<List<InvoiceFullResultDTO>>> InvoiceHeader_RefreshResults(int headerId);
    Task<WebResult<InvoiceUpdateResultDTO>> InvoiceHeader_Update(int headerId, int statusTypeId);
    Task<WebResult<InvoiceHeaderEntity>> InvoiceHeader_Update(int headerId, InvoiceHeaderUpdateDTO update);
    Task<WebResult<List<InvoiceFullResultDTO>>> InvoiceHeader_Update(int headerId, IEnumerable<InvoiceDetailUpdateDTO> updates);
    Task<WebResult<InvoiceSearchDTO>> InvoiceSearch_Get();
    Task<WebResult<InvoiceSearchDTO>> InvoiceSearch_Get(InvoiceFilterDTO filter);
    Task<WebResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Get();
    Task<WebResult<RandomInvoiceDTO>> InvoiceUploader_GetRandom();
    Task<WebResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Upload(FileParameter file);
    Task<WebResult<List<ResultStatusTypeEntity>>> ResultStatusType_Get();
    Task<WebResult<List<StatusTypeEntity>>> StatusType_Get();
}

public class ServiceWrapper : IServiceWrapper
{
    private IClient _client;

    public ServiceWrapper(IClient client)
    {
        _client = client;
    }

    public async Task<WebResult<InvoiceHeaderEntity>> InvoiceHeader_Get(int headerId)
    {
        var result = await _client.ApiInvoiceheaderGetAsync(headerId);
        return new WebResult<InvoiceHeaderEntity>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WebResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Get()
    {
        var result = await _client.ApiInvoiceuploaderGetAsync();
        return new WebResult<List<InvoiceHeaderEntity>>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WebResult<RandomInvoiceDTO>> InvoiceUploader_GetRandom()
    {
        var result = await _client.ApiInvoiceuploaderRandomAsync();
        return new WebResult<RandomInvoiceDTO>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WebResult<InvoiceSearchDTO>> InvoiceSearch_Get()
    {
        var result = await _client.ApiInvoicesearchGetAsync();
        return new WebResult<InvoiceSearchDTO>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WebResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Upload(FileParameter file)
    {
        var result = await _client.ApiInvoiceuploaderPostAsync(file);
        return new WebResult<List<InvoiceHeaderEntity>>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WebResult<InvoiceSearchDTO>> InvoiceSearch_Get(InvoiceFilterDTO filter)
    {
        var result = await _client.ApiInvoicesearchPostAsync(filter);
        return new WebResult<InvoiceSearchDTO>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WebResult<List<InvoiceFullResultDTO>>> InvoiceHeader_GetResults(int headerId)
    {
        var result = await _client.ApiInvoiceheaderResultsGetAsync(headerId);
        return new WebResult<List<InvoiceFullResultDTO>>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WebResult<InvoicePermissionsDTO>> InvoiceHeader_GetPermissions(int headerId)
    {
        var result = await _client.ApiInvoiceheaderPermissionsAsync(headerId);
        return new WebResult<InvoicePermissionsDTO>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WebResult<List<InvoiceFullResultDTO>>> InvoiceHeader_RefreshResults(int headerId)
    {
        var result = await _client.ApiInvoiceheaderResultsPutAsync(headerId);
        return new WebResult<List<InvoiceFullResultDTO>>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WebResult> InvoiceHeader_Delete(int headerId)
    {
        var result = await _client.ApiInvoiceheaderDeleteAsync(headerId);
        return new WebResult()
        {
            IsSuccess = result.Success,
            Message = result.Message
        };
    }

    public async Task<WebResult<List<ResultStatusTypeEntity>>> ResultStatusType_Get()
    {
        var result = await _client.ApiResultstatustypeAsync();
        return new WebResult<List<ResultStatusTypeEntity>>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WebResult<List<StatusTypeEntity>>> StatusType_Get()
    {
        var result = await _client.ApiStatustypeAsync();
        return new WebResult<List<StatusTypeEntity>>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WebResult<InvoiceUpdateResultDTO>> InvoiceHeader_Update(int headerId, int statusTypeId)
    {
        var result = await _client.ApiInvoiceheaderStatusAsync(headerId, statusTypeId);
        return new WebResult<InvoiceUpdateResultDTO>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WebResult<List<InvoiceFullResultDTO>>> InvoiceHeader_Update(int headerId, IEnumerable<InvoiceDetailUpdateDTO> updates)
    {
        var result = await _client.ApiInvoiceheaderDetailAsync(headerId, updates);
        return new WebResult<List<InvoiceFullResultDTO>>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WebResult<InvoiceHeaderEntity>> InvoiceHeader_Update(int headerId, InvoiceHeaderUpdateDTO update)
    {
        var result = await _client.ApiInvoiceheaderHeaderAsync(headerId, update);
        return new WebResult<InvoiceHeaderEntity>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }
}
