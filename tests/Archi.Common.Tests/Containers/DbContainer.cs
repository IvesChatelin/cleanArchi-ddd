using Testcontainers.PostgreSql;

namespace Archi.Common.Tests.Containers;

public static class DbContainer
{
    public static readonly PostgreSqlContainer postgreSqlContainer = new (
        new PostgreSqlConfiguration(
            database: "postgres",
            username: "postgres",
            password: "root-test"
        ) 
    );
    
}