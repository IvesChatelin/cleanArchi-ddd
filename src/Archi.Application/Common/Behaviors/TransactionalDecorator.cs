using Archi.Application.Common.Abstractions.Commands;
using Archi.SharedKernel.Models;
using Archi.SharedKernel.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Archi.Application.Common.Behaviors;

public static class TransactionalDecorator
{
    public sealed class TransactionalCommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        private readonly ICommandHandler<TCommand, TResponse> _innerHandler;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TransactionalCommandHandler<TCommand, TResponse>> _logger;

        public TransactionalCommandHandler(
            ICommandHandler<TCommand, TResponse> innerHandler,
            IUnitOfWork unitOfWork,
            ILogger<TransactionalCommandHandler<TCommand, TResponse>> logger)
        {
            _innerHandler = innerHandler;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            string commandName = typeof(TCommand).Name;

            _logger.LogInformation("Starting transaction for command {Command}", commandName);
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                Result<TResponse> result = await _innerHandler.Handle(command, cancellationToken);

                if (result.IsSuccess)
                {
                    await _unitOfWork.CommitTransactionAsync(cancellationToken);
                    _logger.LogInformation("Transaction committed for command {Command}", commandName);
                }
                else
                {
                    await _unitOfWork.RollBackTransactionAsync(cancellationToken);
                    _logger.LogWarning("Transaction rolled back for command {Command} due to failure", commandName);
                }

                return result;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackTransactionAsync(cancellationToken);
                _logger.LogError(ex, "Transaction rolled back for command {Command} due to exception", commandName);
                throw;  
            }
        }
    }
}