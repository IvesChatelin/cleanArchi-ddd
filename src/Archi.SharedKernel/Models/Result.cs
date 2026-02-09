using Archi.SharedKernel.Errors;

namespace Archi.SharedKernel.Models;

/// <summary>
/// Generic Pattern Result, pourquoi ?
/// - Lorsque l'application peut continuer après l'erreur
/// - Le client prend la décision selon le resultat
/// - l'erreur fait partie du langage métier
/// - L'erreur n'est pas technique
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class Result<T>
{
    public T? Value { get; }

    public Error Error { get; }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    private Result(T value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value), "Value of result cannot be null.");
        IsSuccess = true;
        Error = Error.None;
    }

    private Result(Error error)
    {
        Error = error ?? throw new ArgumentNullException(nameof(error), "Error of result cannot be null.");
        IsSuccess = false;
    }

    public static Result<T> Success(T value) => new(value);

    public static Result<T> Failure(Error error) => new(error);

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure)
    {
        return IsSuccess ? onSuccess(Value!) : onFailure(Error);
    }
}

/// <summary>
/// Non generic Pattern Result, pourquoi ?
/// - Lorsque l'application peut continuer après l'erreur
/// - Le client prend la décision selon le resultat
/// - l'erreur fait partie du langage métier
/// - L'erreur n'est pas technique
/// </summary>
public sealed class Result
{
    public Error Error { get; }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    private Result()
    {
        IsSuccess = true;
        Error = Error.None;
    }

    private Result(Error error)
    {
        Error = error ?? throw new ArgumentNullException(nameof(error), "Error cannot be null.");
        IsSuccess = false;
    }

    public static Result Success() => new();

    public static Result Failure(Error error) => new(error);

    public TResult Match<TResult>(Func<TResult> onSuccess, Func<Error, TResult> onFailure)
    {
        return IsSuccess ? onSuccess() : onFailure(Error!);
    }
}
