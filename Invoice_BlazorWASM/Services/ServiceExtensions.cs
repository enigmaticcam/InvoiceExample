using Invoice_BlazorWASM.Services.Core;
using Invoice_BlazorWASM.Services.ServerCommand;
using Invoice_BlazorWASM.Services.ServerCommand.InvoiceDetail;
using Invoice_BlazorWASM.Services.ServerCommand.InvoiceHeader;
using Invoice_BlazorWASM.Services.ServerCommand.InvoiceSearch;
using Invoice_BlazorWASM.Services.ServerCommand.InvoiceUploader;

namespace Invoice_BlazorWASM.Services;

public static class ServiceExtensions
{
    public static void UseInvoice(this IServiceCollection services)
    {
        services.AddScoped<ClipboardService>();
        services.AddScoped<IClearCollection, ClearCollection>();
        services.AddScoped<IFileDownload, FileDownload>();
        services.AddScoped<IInvoiceDetailInvoker, InvoiceDetailInvoker>();
        services.AddScoped<IInvoiceDetailState, InvoiceDetailState>();
        services.AddScoped<IInvoiceHeaderInvoker, InvoiceHeaderInvoker>();
        services.AddScoped<IInvoiceHeaderState, InvoiceHeaderState>();
        services.AddScoped<IInvoiceSearchInvoker, InvoiceSearchInvoker>();
        services.AddScoped<IInvoiceUploaderInvoker, InvoiceUploaderInvoker>();
        services.AddScoped<IInvoiceUploaderState, InvoiceUploaderState>();
        services.AddScoped<IServerInvoker, ServerInvoker>();
        services.AddScoped<IServerStatus, ServerStatus>();
        services.AddScoped<IServiceWrapper, ServiceWrapper>();
    }
}
