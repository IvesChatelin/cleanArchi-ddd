using Archi.Domain.Common.Models;

namespace Archi.Application.Common.Abstractions.Queries;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handler (TQuery query, CancellationToken ct);
}
