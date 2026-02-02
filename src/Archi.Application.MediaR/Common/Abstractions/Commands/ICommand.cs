using Archi.SharedKernel.Models;
using MediatR;

namespace Archi.Application.MediaR.Common.Abstractions.Commands;

public interface ICommand : IRequest<Result>{}

public interface ICommand<TResponse>: IRequest<Result<TResponse>>{}