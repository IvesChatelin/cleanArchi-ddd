using Archi.Application;
using Archi.Infrastructure;
using Archi.Presentation;
using Archi.Presentation.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Register Serilog
builder.AddSerilogConfiguration();

Log.Information("Starting up the application...");

// Register services
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();

var app = builder.Build();

// Pipeline middleware configuration
app.UseSerilogRequestLogging();

app.MapGet("/", () => "Hello World!");

app.Run();



/*namespace Archi.Presentation
{
    public partial class Program;
}*/
