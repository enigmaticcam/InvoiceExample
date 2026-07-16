namespace Invoice_BlazorWASM.Services.Core;

public interface IServiceWrapper
{
    Task<BlazorResult<InvoiceHeaderEntity>> InvoiceHeader_Get(int headerId);
    Task<BlazorResult<InvoiceSearchDTO>> InvoiceSearch_Get();
    Task<BlazorResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Get();
    Task<BlazorResult<RandomInvoiceDTO>> InvoiceUploader_GetRandom();
}

public class ServiceWrapper : IServiceWrapper
{
    private IClient _client;

    public ServiceWrapper(IClient client)
    {
        _client = client;
    }

    public async Task<BlazorResult<InvoiceHeaderEntity>> InvoiceHeader_Get(int headerId)
    {
        var result = await _client.ApiInvoiceheaderAsync(headerId);
        return new BlazorResult<InvoiceHeaderEntity>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<BlazorResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Get()
    {
        var result = await _client.ApiInvoiceuploaderGetAsync();
        return new BlazorResult<List<InvoiceHeaderEntity>>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<BlazorResult<RandomInvoiceDTO>> InvoiceUploader_GetRandom()
    {
        var result = await _client.ApiInvoiceuploaderRandomAsync();
        return new BlazorResult<RandomInvoiceDTO>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }

    public async Task<BlazorResult<InvoiceSearchDTO>> InvoiceSearch_Get()
    {
        var result = await _client.ApiInvoicesearchGetAsync();
        return new BlazorResult<InvoiceSearchDTO>()
        {
            IsSuccess = result.Success,
            Message = result.Message,
            Obj = result.Obj
        };
    }
}
