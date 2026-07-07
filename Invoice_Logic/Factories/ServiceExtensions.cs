using Invoice_Logic.API;
using Microsoft.Extensions.DependencyInjection;

namespace Invoice_Logic.Factories;

public static class ServiceExtensions
{
    public static void UseInvoice(this IServiceCollection services)
    {
        services.AddScoped<IAPICaller, APICaller>();
        services.AddScoped<IFactoryMain, FactoryMain>();
    }
}
