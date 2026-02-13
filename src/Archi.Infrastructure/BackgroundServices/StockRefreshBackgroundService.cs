using Microsoft.Extensions.Hosting;

namespace Archi.Infrastructure.BackgroundServices;

public class StockRefreshBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // logic
            await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken); // wait 30 min
        }
    }
}