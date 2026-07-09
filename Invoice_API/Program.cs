using Invoice_API;
using Invoice_Logic.Caching;
using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Data.EF;
using Invoice_Logic.Factories;
using Microsoft.EntityFrameworkCore;

string corsPolicy = "corsPolicy";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
        policy =>
        {
            policy.WithOrigins("https://localhost:7206")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(setup =>
//{
//    setup.UseAllOfToExtendReferenceSchemas();
//    setup.MapType<decimal>(() => new OpenApiSchema { Type = JsonSchemaType.Number, Format = "decimal" });
//});
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<Invoice_Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"), o => o.UseCompatibilityLevel(120));
});
builder.Services.AddScoped(x => new CacheOptions()
{
    AbsoluteExpirationInSeconds = builder.Configuration.GetSection("CacheOptions").GetValue<int>("AbsoluteExpirationInSeconds"),
    SlidingExpirationInSeconds = builder.Configuration.GetSection("CacheOptions").GetValue<int>("SlidingExpirationInSeconds")
});
builder.Services.UseInvoice();
builder.Services.AddOpenApi();
builder.Services.AddOptions<WebServerDTO>()
    .BindConfiguration("WebServer");

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Version One");
    });
}
app.UseHttpsRedirection();
app.UseCors(corsPolicy);
app.MapHealthChecks("/health")
    .AllowAnonymous();
app.ConfigureApi();
app.Run();