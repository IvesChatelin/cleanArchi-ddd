using Archi.Application.Common.Abstractions.Commands;
using Archi.SharedKernel.Errors;
using Archi.SharedKernel.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Archi.Application.Common.Behaviors;

public static class ValidationDecorator
{
    public sealed class ValidationCommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        private readonly ICommandHandler<TCommand, TResponse> _innerHandler;
        private readonly IEnumerable<IValidator<TCommand>>  _validator;

        public ValidationCommandHandler(ICommandHandler<TCommand, TResponse> innerHandler, IEnumerable<IValidator<TCommand>> validator)
        {
            _innerHandler = innerHandler;
            _validator = validator;
        }

        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            ValidationFailure[] validationFailures = await ValidateAsync(command, _validator, cancellationToken);
            
            if (validationFailures.Length == 0)
            {
                return await _innerHandler.Handle(command, cancellationToken);
            }

            return Result<TResponse>.Failure(CreateValidationError(validationFailures));
            
        }

        private async Task<ValidationFailure[]> ValidateAsync(TCommand command, IEnumerable<IValidator<TCommand>>  validator, CancellationToken cancellationToken)
        {
            if (!validator.Any())
            {
                return [];
            }

            var context = new ValidationContext<TCommand>(command);

            ValidationResult[] validationResults = await Task.WhenAll(
                validator.Select(validator => validator.ValidateAsync(context, cancellationToken)));

            ValidationFailure[] validationFailures = [.. validationResults
                .Where(validationResults => !validationResults.IsValid)
                .SelectMany(validationResults => validationResults.Errors)];

            return validationFailures;
        }

        private static ValidationError CreateValidationError(ValidationFailure[] validationFailures) =>
            new([.. validationFailures.Select(f => Error.Validation(f.ErrorCode, f.ErrorMessage))]);
    }
}