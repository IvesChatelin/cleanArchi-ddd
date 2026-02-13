using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;

namespace Archi.Infrastructure.Stock;

public class InMemoryCacheStock
{
    private readonly IMemoryCache _cache;
    private static readonly ConcurrentDictionary<(string, string), SemaphoreSlim> _locks = new ();

    public InMemoryCacheStock(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<int> GetOrSetAsync(string site, string item){

        if (_cache.TryGetValue((site, item), out int qty))
            return qty;

        var semaphore = _locks.GetOrAdd((site, item), _ => new SemaphoreSlim (1, 1));
        var acquiredLock = await semaphore.WaitAsync(TimeSpan.FromSeconds(5));

        if (!acquiredLock)
        {
            throw new Exception ($"Unable to acquire the lock to retrieve the stock of item {item}");
        }

        try
        {
            if (_cache.TryGetValue((site, item), out qty))
                return qty;

            // fetch
            return _cache.Set((site, item), 10);
        }
        finally
        {
            semaphore.Release();
        }
    }

    public async Task Decrement(string site, string item, int qty)
    {
        var semaphore = _locks.GetOrAdd((site, item), _ => new SemaphoreSlim (1, 1));
        var acquiredLock = await semaphore.WaitAsync(TimeSpan.FromSeconds(5));

        if (!acquiredLock)
        {
            throw new Exception ($"Unable to acquire the lock to Decrement the stock of item {item}");
        }

        try
        {
            if (_cache.TryGetValue((site, item), out int oldQty))
            {
                // un contrôle avant ça
                _cache.Set((site, item), oldQty - qty);
            }
        }
        finally
        {
            semaphore.Release();
        }
    }
}