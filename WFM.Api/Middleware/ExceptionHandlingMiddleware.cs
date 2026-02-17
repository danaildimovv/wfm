using WFM.Api.Exceptions;

namespace WFM.Api.Middleware;
public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ResourceInUseException ex)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsJsonAsync(new
            {
                code = "RESOURCE_IN_USE",
                message = ex.Message
            });
        }
        catch (ArgumentNullException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new
            {
                code = "NOT_FOUND",
                message = ex.Message
            });
        }
        catch (AlreadyExistsException ex)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsJsonAsync(new
            {
                code = "RESOURCE_EXISTS",
                message = ex.Message
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new
            {
                code = "UNAUTHORIZED",
                message = ex.Message
            });
        }
    }
}