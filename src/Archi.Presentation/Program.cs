using Archi.Application;
using Archi.Infrastructure;
using Archi.Presentation;
using Archi.Presentation.Extensions;
using Archi.Presentation.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Register Serilog
builder.AddSerilogConfiguration();

// Register services
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();

builder.Services.AddSwagger();
builder.Services.AddEndpoints();
builder.Services.AddCustomizeProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();
builder.Services.AddHealthChecks();
builder.Services.AddValidation(); // pour la vidation des records request

var app = builder.Build();

// Pipeline middleware configuration
app.UseSerilogRequestLogging();
app.UseSwaggerWithUi();
app.MapEndpoints();
app.UseExceptionHandler();

//app.MapGet("/", () => "Hello World!");
app.MapHealthChecks("/health");

app.Run();



/*namespace Archi.Presentation
{
    public partial class Program;
}*/
