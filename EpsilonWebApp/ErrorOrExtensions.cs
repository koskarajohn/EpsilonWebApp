using ErrorOr;

namespace EpsilonWebApp;

public static class ErrorOrExtensions
{

    public static IResult ToResult<T>(this ErrorOr<T> errorOr)
    {
        return errorOr.Match(
            value => Results.Ok(value),
            errors => Problem(errors));
    }

    public static IResult Problem(List<Error> errors)
    {
        var firstError = errors.First();

        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };
        
        return Results.Problem(
            statusCode: statusCode,
            title: firstError.Description,
            extensions: new Dictionary<string, object?>
            {
                ["errors"] = errors.Select(e => new
                {
                    e.Code,
                    e.Description,
                    e.Type
                })
            }
        );
    }
}