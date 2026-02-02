using Archi.SharedKernel.Models;

namespace Archi.Application.Common.Abstractions.Commands;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task<Result> Handle(TCommand request, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    Task<Result<TResponse>> Handle(TCommand request, CancellationToken cancellationToken);
}
