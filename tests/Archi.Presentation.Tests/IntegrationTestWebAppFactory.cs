using Archi.Common.Tests.Containers;
using Archi.Infrastructure.Persistence.EFCore;
using Archi.Presentation.Tests.HostedServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
            services.ReplaceDbOptions(postgreSqlContainer: DbContainer.postgreSqlContainer);
            services.AddHostedService<DbInitializerHostedService>(); 
        });
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await DbContainer.postgreSqlContainer.DisposeAsync();
    }
}