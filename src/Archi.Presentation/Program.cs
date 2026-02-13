using Archi.Application;
using Archi.Infrastructure;
using Archi.Presentation.Extensions;
using Archi.Presentation.Middlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Register Serilog
builder.AddSerilogConfiguration();

// Register services
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddSwagger();
builder.Services.AddEndpoints();
builder.Services.AddCustomizeProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();
builder.Services.AddCustomizeHealthChecks();
builder.Services.AddValidation(); // pour la vidation des records request
builder.Services.AddMemoryCache();

var app = builder.Build();

// Pipeline middleware configuration
app.UseSerilogRequestLogging();
app.UseSwaggerWithUi();
app.MapEndpoints();
app.UseExceptionHandler();


app.MapHealthChecks("/healthz/ready", new HealthCheckOptions
{
    Predicate = healthCheck => healthCheck.Tags.Contains("ready")
});

app.MapHealthChecks("/healthz/live", new HealthCheckOptions
{
    Predicate = _ => false
});

app.Run();

namespace Archi.Presentation
{
    public partial class Program {};
}
