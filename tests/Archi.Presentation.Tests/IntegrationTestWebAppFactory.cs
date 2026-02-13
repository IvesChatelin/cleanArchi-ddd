using Archi.Common.Tests.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace Archi.Presentation.Tests;

public sealed class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public async Task InitializeAsync()
    {
        await DbContainer.postgreSqlContainer.StartAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveDbContext(connectionString: DbContainer.postgreSqlContainer.GetConnectionString());
        });
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await DbContainer.postgreSqlContainer.StopAsync();
    }
}