using Travel.Domain.Core.Exceptions;
using Travel.Domain.Service.Exceptions;

namespace Travel.EndPoint.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BuisnessRuleException ex)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync($"Business Rule Error: {ex.Message}");
        }
        catch (CommandValidationException ex)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync($"validation Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.
            await context.Response.WriteAsync($"Internal Error: {ex.Message}");
        }
    }
}
