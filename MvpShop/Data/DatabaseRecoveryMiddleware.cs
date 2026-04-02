using Microsoft.AspNetCore.Http.Extensions;

namespace MvpShop.Data;

public sealed class DatabaseRecoveryMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, DatabaseRecoveryService recoveryService)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception) when (!context.Response.HasStarted)
        {
            var recovered = await recoveryService.TryRecoverMissingDatabaseAsync(exception, context.RequestAborted);

            if (!recovered)
            {
                throw;
            }

            if (HttpMethods.IsGet(context.Request.Method) || HttpMethods.IsHead(context.Request.Method))
            {
                context.Response.Redirect(context.Request.GetEncodedPathAndQuery());
                return;
            }

            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await context.Response.WriteAsync("Database was restored. Repeat the request.", context.RequestAborted);
        }
    }
}
