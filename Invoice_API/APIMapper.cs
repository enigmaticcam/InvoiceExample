using Invoice_Logic.API;
using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Data.DTOs.Update;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_API;

public static class APIMapper
{
    public static void ConfigureApi(this WebApplication app)
    {
        app.MapGet("/api/invoiceheader/{id:int}", InvoiceHeader_Get);
        app.MapPut("/api/invoiceheader/{id:int}/status", InvoiceHeader_UpdateStatus);
        app.MapPut("/api/invoiceheader/{id:int}/header", InvoiceHeader_Update);
        app.MapDelete("/api/invoiceheader/{id:int}", InvoiceHeader_Delete);
        app.MapGet("/api/invoiceheader/{id:int}/results", InvoiceHeader_GetResults);
        app.MapPut("/api/invoiceheader/{id:int}/results", InvoiceHeader_RefreshResults);
        app.MapPut("/api/invoiceheader/{id:int}/detail", InvoiceHeader_UpdateDetail);
        app.MapGet("/api/invoiceheader/{id:int}/permissions", InvoiceHeader_GetPermissions);

        app.MapGet("/api/invoicesearch", InvoiceSearch_Get);
        app.MapPost("/api/invoicesearch", InvoiceSearch_GetWithFilter);

        app.MapGet("/api/invoiceuploader", InvoiceUploader_Get);
        app.MapGet("/api/invoiceuploader/random", InvoiceUploader_GetRandom);
        app.MapPost("/api/invoiceuploader", InvoiceUploader_Create)
            .DisableAntiforgery();
        app.MapGet("/api/invoiceuploader/template", InvoiceUploader_GetBlankTemplate);

        app.MapGet("/api/resultstatustype", ResultStatusType_Get);
        app.MapGet("/api/statustype", StatusType_Get);
    }

    private static async Task<APIResult> InvoiceHeader_Delete(int id, IAPICaller caller)
    {
        var result = await caller.InvoiceHeader_Delete(id);
        return result;
    }

    private static async Task<APIResult<InvoiceHeaderEntity>> InvoiceHeader_Get(int id, IAPICaller caller)
    {
        var result = await caller.InvoiceHeader_Get(id);
        return result;
    }

    private static async Task<APIResult<List<InvoiceFullResultDTO>>> InvoiceHeader_GetResults(int id, IAPICaller caller)
    {
        var result = await caller.InvoiceHeader_GetResults(id);
        return result;
    }

    private static async Task<APIResult<InvoicePermissionsDTO>> InvoiceHeader_GetPermissions(int id, IAPICaller caller)
    {
        var result = await caller.InvoiceHeader_GetPermissions(id);
        return result;
    }

    private static async Task<APIResult<List<InvoiceFullResultDTO>>> InvoiceHeader_RefreshResults(int id, IAPICaller caller)
    {
        var result = await caller.InvoiceHeader_RefreshResults(id);
        return result;
    }

    private static async Task<APIResult<InvoiceUpdateResultDTO>> InvoiceHeader_UpdateStatus(int id, int statusTypeId, IAPICaller caller)
    {
        var result = await caller.InvoiceHeader_Update(id, statusTypeId);
        return result;
    }

    private static async Task<APIResult<InvoiceHeaderEntity>> InvoiceHeader_Update(int id, InvoiceHeaderUpdateDTO update, IAPICaller caller)
    {
        var result = await caller.InvoiceHeader_Update(id, update);
        return result;
    }

    private static async Task<APIResult<List<InvoiceFullResultDTO>>> InvoiceHeader_UpdateDetail(int id, [FromBody] IEnumerable<InvoiceDetailUpdateDTO> updates, IAPICaller caller)
    {
        var result = await caller.InvoiceHeader_Update(id, updates);
        return result;
    }

    private static async Task<APIResult<InvoiceSearchDTO>> InvoiceSearch_Get(IAPICaller caller)
    {
        var result = await caller.InvoiceSearch_Get();
        return result;
    }

    private static async Task<APIResult<RandomInvoiceDTO>> InvoiceUploader_GetRandom(IAPICaller caller)
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

    private static async Task<APIResult<List<InvoiceHeaderEntity>>> InvoiceUploader_Create(IFormFile file, IAPICaller caller)
    {
        using var stream = file.OpenReadStream();
        var result = await caller.InvoiceUploader_Create(stream);
        return result;
    }

    private static async Task<IResult> InvoiceUploader_GetBlankTemplate(IAPICaller caller)
    {
        var result = await caller.InvoiceUploader_GetBlankTemplate();
        if (result.Success && result.Obj != null)
        {
            return Results.File(result.Obj, fileDownloadName: "Template.xlsx");
        }
        else
        {
            return Results.BadRequest(result.Message);
        }
    }

    private static async Task<APIResult<List<ResultStatusTypeEntity>>> ResultStatusType_Get(IAPICaller caller)
    {
        var result = await caller.ResultStatusType_Get();
        return result;
    }

    private static async Task<APIResult<List<StatusTypeEntity>>> StatusType_Get(IAPICaller caller)
    {
        var result = await caller.StatusType_Get();
        return result;
    }
}
