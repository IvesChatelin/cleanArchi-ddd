using Archi.SharedKernel.Models;

namespace Archi.SharedKernel.Errors;

public sealed record ValidationError : Error
{
    public Error[] Errors { get; }

    public ValidationError(Error[] errors) : base(
        "ValidationError.MultipleErrors",
        "Multiple validation errors occurred.",
        ErrorType.Validation
    )
    {
        Errors = errors;
    }

    public static ValidationError FromResult(IEnumerable<Result> results) 
        => new ([.. results.Where(r => r.IsFailure).Select(r => r.Error)]);
}