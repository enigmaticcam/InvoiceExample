using Invoice_Logic.API;
using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Data.DTOs.Entity;

namespace Invoice_API;

public static class APIMapper
{
    public static void ConfigureApi(this WebApplication app)
    {
        app.MapGet("/api/invoiceheader/{id:int}", InvoiceHeader_Get);
        app.MapGet("/api/invoicesearch", InvoiceSearch_Get);
        app.MapPost("/api/invoicesearch", InvoiceSearch_GetWithFilter);
    }

    private static async Task<APIResult<InvoiceHeaderEntity>> InvoiceHeader_Get(int id, IAPICaller caller)
    {
        var result = await caller.InvoiceHeader_Get(id);
        return result;
    }

    private static async Task<APIResult<InvoiceSearchDTO>> InvoiceSearch_Get(IAPICaller caller)
    {
        var result = await caller.InvoiceSearch_Get();
        return result;
    }

    private static async Task<APIResult<InvoiceSearchDTO>> InvoiceSearch_GetWithFilter(InvoiceFilterDTO filter, IAPICaller caller)
    {
        var result = await caller.InvoiceSearch_Get(filter);
        return result;
    }
}
