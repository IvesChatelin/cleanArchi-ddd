using Archi.Application.Common.Abstractions;
using Archi.SharedKernel.Models;
using Microsoft.Extensions.Logging;

namespace Archi.Infrastructure.Stock;

public class EManagerStockProvider : IStockProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<EManagerStockProvider> _logger;

    public EManagerStockProvider(ILogger<EManagerStockProvider> logger)
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("")
        };
        _logger = logger;

    }

    public Task<Result> GetAllItemsAsync(Guid siteId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<object>> GetStockAsync(Guid ItemId, Guid siteId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> IsTransitAsync(Guid ItemId)
    {
        throw new NotImplementedException();
    }
}