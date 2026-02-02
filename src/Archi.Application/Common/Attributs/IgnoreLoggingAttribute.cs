using System.Diagnostics.CodeAnalysis;
using Serilog.Core;
using Serilog.Events;

namespace Archi.Application.Common.Attributs;

[AttributeUsage(AttributeTargets.Property)]
public class IgnoreLoggingAttribute : Attribute;


public class IgnoreLoggingDestructuringPolicy : IDestructuringPolicy
{
    public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, [NotNullWhen(true)] out LogEventPropertyValue? result)
    {
        var type = value.GetType();
        var ignoreProperties = type.GetProperties()
            .Where(prop => Attribute.IsDefined(prop, typeof(IgnoreLoggingAttribute)))
            .ToList();

        result = null;

        if (ignoreProperties.Count == 0)
        {
            return false;
        }

        var logEventProperties = new List<LogEventProperty>();

        foreach (var prop in ignoreProperties)
        {
            if (type.GetProperty(prop.Name) == null)
            {
                // ScalarValue retroune une valeur simple dans les logs
                var logEventProperty = new LogEventProperty(nameof(prop), new ScalarValue(null));
                logEventProperties.Add(logEventProperty);
            }
            else
            {
                // CreatePropertyValue retourne une valeur complexe (un objet en json) dans les logs
                var logEventProperty = new LogEventProperty(nameof(prop), propertyValueFactory.CreatePropertyValue(nameof(IgnoreLoggingAttribute), true));
                logEventProperties.Add(logEventProperty);
            }
        }

        // StructureValue retourne un objet structuré dans les logs
        result = new StructureValue(logEventProperties);
        return true;
    }
} 