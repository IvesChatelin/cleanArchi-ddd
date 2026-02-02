using Npgsql;

namespace Archi.Infrastructure.Persistence.Configurations;

public class PostgresOptions
{
    public const string SectionName = "Postgres";

    public required string Host { get; set; } = string.Empty;
    public required int Port { get; set; } 
    public required string Database { get; set; } = string.Empty;
    public required string Username { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;

    public string ConnectionString()
    {
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = Host,
            Port = Port,
            Database = Database,
            Username = Username,
            Password = Password
        };

        return builder.ConnectionString;
    }
}
