using DotNet.Testcontainers.Builders;
using Testcontainers.PostgreSql;

namespace Archi.Common.Tests.Containers;

public static class DbContainer
{
# pragma warning disable CS0618
    public static readonly PostgreSqlContainer postgreSqlContainer = new PostgreSqlBuilder()
        .WithDatabase("postgres")
        .WithUsername("postgres")
        .WithPassword("root-test")
        .WithPortBinding(5433)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilExternalTcpPortIsAvailable(5433))
        .WithName(AppDomain.CurrentDomain.FriendlyName)
        .Build();
    
}