using Invoice_Logic.API;
using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Data.DTOs.Entity;

namespace Invoice_API;

public static class APIMapper
{
    public static void ConfigureApi(this WebApplication app)
    {
        app.MapGet("/api/invoiceheader/{id:int}", InvoiceHeader_Get);
        app.MapGet("/api/invoiceheader/{id:int}/detail", InvoiceDetail_Get);
        app.MapPut("/api/invoiceheader/{id:int}/refreshresults", InvoiceHeader_RefreshResults);

        app.MapGet("/api/invoicesearch", InvoiceSearch_Get);
        app.MapPost("/api/invoicesearch", InvoiceSearch_GetWithFilter);

        app.MapGet("/api/invoiceuploader", InvoiceUploader_Get);
        app.MapGet("/api/invoiceuploader/random", InvoiceUploader_GetRandom);
        app.MapPost("/api/invoiceuploader", InvoiceUploader_Create);
    }

    private static async Task<APIResult<List<InvoiceDetailEntity>>> InvoiceDetail_Get(int headerId, IAPICaller caller)
    {
        var result = await caller.InvoiceDetail_Get(headerId);
        return result;
    }

    private static async Task<APIResult<InvoiceHeaderEntity>> InvoiceHeader_Get(int id, IAPICaller caller)
    {
        var result = await caller.InvoiceHeader_Get(id);
        return result;
    }

    private static async Task<APIResult<List<InvoiceDetailEntity>>> InvoiceHeader_RefreshResults(int id, IAPICaller caller)
    {
        var result = await caller.InvoiceHeader_RefreshResults(id);
        return result;
    }

    private static async Task<APIResult<InvoiceSearchDTO>> InvoiceSearch_Get(IAPICaller caller)
    {
        var result = await caller.InvoiceSearch_Get();
        return result;
    }

    private static async Task<APIResult<string>> InvoiceUploader_GetRandom(IAPICaller caller)
    {
        var result = await caller.InvoiceUploader_GetRandom();
        return result;
    }

    private static async Task<APIResult<InvoiceSearchDTO>> InvoiceSearch_GetWithFilter(InvoiceFilterDTO filter, IAPICaller caller)
    {
        var result = await caller.InvoiceSearch_Get(filter);
        return result;
    }

    private static async Task<APIResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Get(IAPICaller caller)
    {
        var result = await caller.InvoiceUploader_Get();
        return result;
    }

    private static async Task<APIResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Create(Stream stream, IAPICaller caller)
    {
        var result = await caller.InvoiceUploader_Create(stream);
        return result;
    }
}
