using Archi.SharedKernel.Models;
using MediatR;

namespace Archi.Application.MediaR.Common.Abstractions.Commands;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
    new Task<Result> Handle(TCommand request, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
    new Task<Result<TResponse>> Handle(TCommand request, CancellationToken cancellationToken);
}
