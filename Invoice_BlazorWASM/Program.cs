using Invoice_BlazorWASM;
using Invoice_BlazorWASM.Services;
using Invoice_BlazorWASM.Services.Core;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IClient, Client>("Invoice.ServerAPI");
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("Invoice.ServerAPI"));

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
});
builder.Services.AddScoped<IAPIConnection, APIConnectionDev>();
builder.Services.UseInvoice();

await builder.Build().RunAsync();
