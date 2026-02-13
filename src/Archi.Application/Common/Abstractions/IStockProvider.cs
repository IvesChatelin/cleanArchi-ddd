using Archi.SharedKernel.Models;

namespace Archi.Application.Common.Abstractions;

public interface IStockProvider
{
    Task<Result> GetAllItemsAsync(Guid siteId);
    Task<Result<object>> GetStockAsync(Guid ItemId, Guid siteId);
    Task<Result<bool>> IsTransitAsync(Guid ItemId);
}