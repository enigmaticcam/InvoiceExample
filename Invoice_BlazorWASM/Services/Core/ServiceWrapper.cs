namespace Invoice_BlazorWASM.Services.Core;

public interface IServiceWrapper
{
    Task<BlazorResult<InvoiceHeaderEntity>> InvoiceHeader_Get(int headerId);
    Task<BlazorResult<InvoiceSearchDTO>> InvoiceSearch_Get();
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
