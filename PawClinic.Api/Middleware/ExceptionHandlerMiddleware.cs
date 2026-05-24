using System.Net;
using System.Text.Json;
using PawClinic.Application.Exceptions;

namespace PawClinic.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            context.Response.ContentType = "application/json";

            var (statusCode, message, errors) = exception switch
            {
                ValidationException ve => (HttpStatusCode.BadRequest, "Validation failed", ve.ValidationErrors),
                NotFoundException => (HttpStatusCode.NotFound, exception.Message, (List<string>?)null),
                BadRequestException => (HttpStatusCode.BadRequest, exception.Message, null),
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.", null)
            };

            context.Response.StatusCode = (int)statusCode;

            var response = new { message, errors };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
            => app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
