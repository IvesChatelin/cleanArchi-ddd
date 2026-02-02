using Archi.Application.Common.Abstractions.Commands;
using Archi.Domain.Common.Models;
using Microsoft.Extensions.Logging;

namespace Archi.Application.Common.Behaviors;

public static class LoggingDecorator
{
    public sealed class CommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        private readonly ILogger<CommandHandler<TCommand, TResponse>> _logger;
        private readonly ICommandHandler<TCommand, TResponse> _innerHandler;

        public CommandHandler(
            ILogger<CommandHandler<TCommand, TResponse>> logger,
            ICommandHandler<TCommand, TResponse> innerHandler)
        {
            _logger = logger;
            _innerHandler = innerHandler;
        }

        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            string commandName = typeof(TCommand).Name;

            _logger.LogInformation("Processing command {Command}", commandName);

            Result<TResponse> result = await _innerHandler.Handle(command, cancellationToken);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Completed command {Command}", commandName);
            }
            else
            {
                _logger.LogError("Completed command {Command} with error", commandName);
            }

            return result;
        }
    }

    public sealed class CommandHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ILogger<CommandHandler<TCommand>> _logger;
        private readonly ICommandHandler<TCommand> _innerHandler;

        public CommandHandler(
            ILogger<CommandHandler<TCommand>> logger,
            ICommandHandler<TCommand> innerHandler)
        {
            _logger = logger;
            _innerHandler = innerHandler;
        }

        public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
        {
            string commandName = typeof(TCommand).Name;

            _logger.LogInformation("Processing command {Command}", commandName);

            Result result = await _innerHandler.Handle(command, cancellationToken);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Completed command {Command}", commandName);
            }
            else
            {
                _logger.LogError("Completed command {Command} with error", commandName);
            }

            return result;
        }
    }
}
