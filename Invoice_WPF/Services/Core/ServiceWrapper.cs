namespace Invoice_WPF.Services.Core;

public interface IServiceWrapper
{
    Task<WPFResult> InvoiceHeader_Delete(int headerId);
    Task<WPFResult<InvoiceHeaderEntity>> InvoiceHeader_Get(int headerId);
    Task<WPFResult<InvoicePermissionsDTO>> InvoiceHeader_GetPermissions(int headerId);
    Task<WPFResult<List<InvoiceFullResultDTO>>> InvoiceHeader_GetResults(int headerId);
    Task<WPFResult<List<InvoiceFullResultDTO>>> InvoiceHeader_RefreshResults(int headerId);
    Task<WPFResult<InvoiceHeaderEntity>> InvoiceHeader_Update(int headerId, int statusTypeId);
    Task<WPFResult<List<InvoiceFullResultDTO>>> InvoiceHeader_Update(int headerId, IEnumerable<InvoiceDetailUpdateDTO> updates);
    Task<WPFResult<InvoiceSearchDTO>> InvoiceSearch_Get();
    Task<WPFResult<InvoiceSearchDTO>> InvoiceSearch_Get(InvoiceFilterDTO filter);
    Task<WPFResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Get();
    Task<WPFResult<RandomInvoiceDTO>> InvoiceUploader_GetRandom();
    Task<WPFResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Upload(FileParameter file);
    Task<WPFResult<List<ResultStatusTypeEntity>>> ResultStatusType_Get();
    Task<WPFResult<List<StatusTypeEntity>>> StatusType_Get();
}

public class ServiceWrapper : IServiceWrapper
{
    private IClient _client;

    public ServiceWrapper(IClient client)
    {
        _client = client;
    }

    public async Task<WPFResult<InvoiceHeaderEntity>> InvoiceHeader_Get(int headerId)
    {
        var result = await _client.ApiInvoiceheaderGetAsync(headerId);
        return new WPFResult<InvoiceHeaderEntity>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WPFResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Get()
    {
        var result = await _client.ApiInvoiceuploaderGetAsync();
        return new WPFResult<List<InvoiceHeaderEntity>>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WPFResult<RandomInvoiceDTO>> InvoiceUploader_GetRandom()
    {
        var result = await _client.ApiInvoiceuploaderRandomAsync();
        return new WPFResult<RandomInvoiceDTO>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WPFResult<InvoiceSearchDTO>> InvoiceSearch_Get()
    {
        var result = await _client.ApiInvoicesearchGetAsync();
        return new WPFResult<InvoiceSearchDTO>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WPFResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Upload(FileParameter file)
    {
        var result = await _client.ApiInvoiceuploaderPostAsync(file);
        return new WPFResult<List<InvoiceHeaderEntity>>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WPFResult<InvoiceSearchDTO>> InvoiceSearch_Get(InvoiceFilterDTO filter)
    {
        var result = await _client.ApiInvoicesearchPostAsync(filter);
        return new WPFResult<InvoiceSearchDTO>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WPFResult<List<InvoiceFullResultDTO>>> InvoiceHeader_GetResults(int headerId)
    {
        var result = await _client.ApiInvoiceheaderResultsGetAsync(headerId);
        return new WPFResult<List<InvoiceFullResultDTO>>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WPFResult<InvoicePermissionsDTO>> InvoiceHeader_GetPermissions(int headerId)
    {
        var result = await _client.ApiInvoiceheaderPermissionsAsync(headerId);
        return new WPFResult<InvoicePermissionsDTO>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WPFResult<List<InvoiceFullResultDTO>>> InvoiceHeader_RefreshResults(int headerId)
    {
        var result = await _client.ApiInvoiceheaderResultsPutAsync(headerId);
        return new WPFResult<List<InvoiceFullResultDTO>>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WPFResult> InvoiceHeader_Delete(int headerId)
    {
        var result = await _client.ApiInvoiceheaderDeleteAsync(headerId);
        return new WPFResult()
        {
            IsSuccess = result.Success,
            Message = result.Message
        };
    }

    public async Task<WPFResult<List<ResultStatusTypeEntity>>> ResultStatusType_Get()
    {
        var result = await _client.ApiResultstatustypeAsync();
        return new WPFResult<List<ResultStatusTypeEntity>>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WPFResult<List<StatusTypeEntity>>> StatusType_Get()
    {
        var result = await _client.ApiStatustypeAsync();
        return new WPFResult<List<StatusTypeEntity>>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WPFResult<InvoiceHeaderEntity>> InvoiceHeader_Update(int headerId, int statusTypeId)
    {
        var result = await _client.ApiInvoiceheaderPutAsync(headerId, statusTypeId);
        return new WPFResult<InvoiceHeaderEntity>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<WPFResult<List<InvoiceFullResultDTO>>> InvoiceHeader_Update(int headerId, IEnumerable<InvoiceDetailUpdateDTO> updates)
    {
        var result = await _client.ApiInvoiceheaderDetailAsync(headerId, updates);
        return new WPFResult<List<InvoiceFullResultDTO>>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }
}
