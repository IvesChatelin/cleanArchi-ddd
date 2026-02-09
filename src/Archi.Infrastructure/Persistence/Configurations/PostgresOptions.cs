using Npgsql;

namespace Archi.Infrastructure.Persistence.Configurations;

public class PostgresOptions
{
    public const string SectionName = "Postgres";

    public required string Host { get; set; }
    public required int Port { get; set; } 
    public required string Database { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }

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
