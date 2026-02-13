using Microsoft.Extensions.Hosting;

namespace Archi.Infrastructure.BackgroundServices;

public class StockPreloaderHostedServices : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}