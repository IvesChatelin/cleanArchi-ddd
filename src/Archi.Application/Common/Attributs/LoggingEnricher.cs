using Serilog.Core;
using Serilog.Events;

namespace Archi.Application.Common.Attributs;

public sealed class LoggingEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        string customLevel = logEvent.Level switch
        {
            LogEventLevel.Verbose => "trace",
            LogEventLevel.Debug => "debug",
            LogEventLevel.Information => "info",
            LogEventLevel.Warning => "warn",
            LogEventLevel.Error => "error",
            LogEventLevel.Fatal => "fatal",
            _ => logEvent.Level.ToString().ToLower()
        };

        LogEventProperty logEventProperty = propertyFactory.CreateProperty("level", customLevel);
        logEvent.AddPropertyIfAbsent(logEventProperty);
    }
}
