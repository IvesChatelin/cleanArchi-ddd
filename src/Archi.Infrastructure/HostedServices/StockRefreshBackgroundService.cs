using Microsoft.Extensions.Hosting;

namespace Archi.Infrastructure.HostedServices;

public class StockRefreshBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // logic
            await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken); // wait 30 min
        }
    }

    // outbox repository 
}