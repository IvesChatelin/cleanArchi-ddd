using System.ComponentModel.DataAnnotations;
using Npgsql;

namespace Archi.Infrastructure.Persistence.Configurations;

public class PostgresOptions
{
    public const string SectionName = "Postgres";

    [Required]
    public required string Host { get; set; }

    [Required]
    public required int Port { get; set; } 

    [Required]
    public required string Database { get; set; }

    [Required]
    public required string Username { get; set; }

    [Required]
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
