using Invoice_Logic.API;
using Invoice_Logic.Data.DTOs.Entity;

namespace Invoice_API;

public static class APIMapper
{
    public static void ConfigureApi(this WebApplication app)
    {
        app.MapGet("/api/invoiceheader/{id:int}", InvoiceHeader_Get);
    }

    private static async Task<APIResult<InvoiceHeaderEntity>> InvoiceHeader_Get(int id, IAPICaller caller)
    {
        var result = await caller.InvoiceHeader_Get(id);
        return result;
    }
}
