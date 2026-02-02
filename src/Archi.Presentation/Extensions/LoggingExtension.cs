using Archi.Application.Common.Attributs;
using Serilog;

namespace Archi.Presentation.Extensions;
public static class LoggingExtension
{
    public static WebApplicationBuilder AddSerilogConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddSerilog((services, loggerConfig) =>
        {
            loggerConfig
                .Enrich.With<LoggingEnricher>()
                .Destructure.With<IgnoreLoggingDestructuringPolicy>()
                .ReadFrom.Configuration(builder.Configuration);
        });

        return builder;
    }
}
