using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FreelancerInvoicing.Tools.Exceptions;

namespace FreelancerInvoicing.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                await HandleExceptionAsync(context, ex.Message, ex.StatusCode, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, "An unexpected error occurred.", 500, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, string message, int statusCode, Exception ex)
        {
            _logger.LogError(ex,
                "Exception | {Method} {Path} | UserId={UserId}",
                context.Request.Method,
                context.Request.Path,
                context.User?.FindFirst("id")?.Value
            );

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var problem = new
            {
                status = statusCode,
                title = message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
        }
    }
}
