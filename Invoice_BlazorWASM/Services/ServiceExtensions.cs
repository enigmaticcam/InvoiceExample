using Invoice_BlazorWASM.Services.Core;
using Invoice_BlazorWASM.Services.Entities.ServerCommand;
using Invoice_BlazorWASM.Services.Entities.ServerCommand.InvoiceHeader;
using Invoice_BlazorWASM.Services.Entities.ServerCommand.InvoiceSearch;
using Invoice_BlazorWASM.Services.Entities.ServerCommand.InvoiceUploader;

namespace Invoice_BlazorWASM.Services;

public static class ServiceExtensions
{
    public static void UseInvoice(this IServiceCollection services)
    {
        services.AddScoped<IClearCollection, ClearCollection>();
        services.AddScoped<IInvoiceHeaderInvoker, InvoiceHeaderInvoker>();
        services.AddScoped<IInvoiceHeaderState, InvoiceHeaderState>();
        services.AddScoped<IInvoiceSearchInvoker, InvoiceSearchInvoker>();
        services.AddScoped<IInvoiceUploaderInvoker, InvoiceUploaderInvoker>();
        services.AddScoped<IServerInvoker, ServerInvoker>();
        services.AddScoped<IServerStatus, ServerStatus>();
        services.AddScoped<IServiceWrapper, ServiceWrapper>();
    }
}
