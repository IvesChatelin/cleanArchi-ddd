using Archi.SharedKernel.Errors;
using Archi.SharedKernel.Models;

namespace Archi.Presentation.Extensions;

public static class ResultExtension
{
    public static IResult Problem(Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return Results.Problem(
            detail: GetDetail(result.Error), 
            statusCode: GetStatusCode(result.Error),
            title: GetTitle(result.Error),
            type: GetType(result.Error),
            extensions: null
        );
        
    }

    static string GetTitle(Error error)
    {
        return error.Type switch
        {
            ErrorType.Failure => error.Code,
            ErrorType.Validation => error.Code,
            ErrorType.NotFound => error.Code,
            ErrorType.Conflict => error.Code,
            ErrorType.Problem => error.Code,
            _ => "Unknown"
        };
    }

    static int GetStatusCode(Error error)
    {
        return error.Type switch
        {
            ErrorType.Failure => StatusCodes.Status400BadRequest,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Problem => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    static string GetType(Error error)
    {
        return error.Type switch
        {
            ErrorType.Failure => "Bad Request",
            ErrorType.NotFound => "Not Found",
            ErrorType.Conflict => "Conflict",
            ErrorType.Validation => "Bad Request",
            ErrorType.Problem => "Internal Server Error",
            _ => "Unknown Error"
        };
    }

    static string GetDetail(Error error)
    {
        return error.Type switch
        {
            ErrorType.Failure => error.Description,
            ErrorType.Validation => error.Description,
            ErrorType.NotFound => error.Description,
            ErrorType.Conflict => error.Description,
            ErrorType.Problem => error.Description,
            _ => "An unexpected error occurred"
        };
    }

    /*static Dictionary<string, object?>? GetExtensions(Error error)
    {
        return new Dictionary<string, object?>()
        {
            { "Code", error.Code },
            { "Type", error.Type.ToString() }
        };
    }*/
}
