using System.Diagnostics;
using Archi.Application.Common.Abstractions.Commands;
using Archi.Application.Common.Abstractions.Queries;
using Archi.SharedKernel.Models;
using Microsoft.Extensions.Logging;

namespace Archi.Application.Common.Behaviors;

public static class LoggingDecorator
{
    public sealed class LoggingCommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        private readonly ILogger<LoggingCommandHandler<TCommand, TResponse>> _logger;
        private readonly ICommandHandler<TCommand, TResponse> _innerHandler;

        public LoggingCommandHandler(
            ILogger<LoggingCommandHandler<TCommand, TResponse>> logger,
            ICommandHandler<TCommand, TResponse> innerHandler)
        {
            _logger = logger;
            _innerHandler = innerHandler;
        }

        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            string commandName = typeof(TCommand).Name;

            _logger.LogInformation("Processing command {Command}", commandName);

            var sw = Stopwatch.StartNew();

            Result<TResponse> result = await _innerHandler.Handle(command, cancellationToken);

            sw.Stop();

            if (result.IsSuccess)
            {
                _logger.LogInformation("Completed command {Command} in {Elapsed} ms", commandName, sw.ElapsedMilliseconds);
            }
            else
            {
                _logger.LogError("Completed command {Command} with error in {Elapsed} ms", commandName, sw.ElapsedMilliseconds);
            }

            return result;
        }
    }

    public sealed class LoggingCommandHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ILogger<LoggingCommandHandler<TCommand>> _logger;
        private readonly ICommandHandler<TCommand> _innerHandler;

        public LoggingCommandHandler(
            ILogger<LoggingCommandHandler<TCommand>> logger,
            ICommandHandler<TCommand> innerHandler)
        {
            _logger = logger;
            _innerHandler = innerHandler;
        }

        public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
        {
            string commandName = typeof(TCommand).Name;

            _logger.LogInformation("Processing command {Command}", commandName);

            var sw = Stopwatch.StartNew();

            Result result = await _innerHandler.Handle(command, cancellationToken);

            sw.Stop();

            if (result.IsSuccess)
            {
                _logger.LogInformation("Completed command {Command} in {Elapsed} ms", commandName, sw.ElapsedMilliseconds);
            }
            else
            {
                _logger.LogError("Completed command {Command} with error in {Elapsed} ms", commandName, sw.ElapsedMilliseconds);
            }

            return result;
        }
    }

    public sealed class LoggingQueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        private ILogger<LoggingQueryHandler<TQuery, TResponse>> _logger;
        private IQueryHandler<TQuery, TResponse> _innerQuery;

        public LoggingQueryHandler(ILogger<LoggingQueryHandler<TQuery, TResponse>> logger,
            IQueryHandler<TQuery, TResponse> innerQuery)
        {
            _logger = logger;
            _innerQuery = innerQuery;
        }

        public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken)
        {
            string queryName = typeof(TQuery).Name;

            _logger.LogInformation("Processing query {query}", queryName);

            var sw = Stopwatch.StartNew();

            Result<TResponse> result = await _innerQuery.Handle(query, cancellationToken);

            sw.Stop();

            if (result.IsSuccess)
            {
                _logger.LogInformation("Completed query {query} in {Elapsed} ms", queryName, sw.ElapsedMilliseconds);

            }else
            {
                _logger.LogInformation("Completed query {query} with error in {Elapsed} ms", queryName, sw.ElapsedMilliseconds);
            }

            return result;
        }
    }
}
