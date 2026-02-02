namespace Archi.SharedKernel.Errors;

/// <summary>
/// Classe staric des erreurs liées à un domaine
/// </summary>
public static class DomainError
{
    public static readonly Error WithoutParameter = Error.Failure(
        "DomainError.WithoutParameter",
        "Error related to domain without parameter"
    );

    public static Error WithParameter(int Id) => Error.Failure(
        "DomainError.WithParameter",
        $"Error related to domain with {Id} Parameter"
    );

    
}
