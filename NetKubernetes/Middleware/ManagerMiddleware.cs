using System.Net;
using Newtonsoft.Json;

namespace NetKubernetes.Middleware;

public class ManagerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ManagerMiddleware> _logger;

    public ManagerMiddleware(RequestDelegate next, ILogger<ManagerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await ManagerExceptionAsync(context, e, _logger);
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task ManagerExceptionAsync(HttpContext context, Exception ex,
        ILogger<ManagerMiddleware> logger)
    {
        object? errors = null;

        switch (ex)
        {
            case MiddlewareException me:
                logger.LogError(ex, "Middleware Error");
                errors = me.Errors;
                context.Response.StatusCode = (int)me.Code;
                break;
            
            case Exception e:
                logger.LogError(ex, "Server Error");
                errors = string.IsNullOrWhiteSpace(e.Message)  ? "Error" : e.Message;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }
        
        context.Response.ContentType = "application/json";

        var results = string.Empty;
        if (errors != null)
        {
            results = JsonConvert.SerializeObject(new { errors });
        }

        await context.Response.WriteAsync(results);
    }
}