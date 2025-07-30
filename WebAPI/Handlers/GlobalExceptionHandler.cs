using System.Net;
using Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace WebAPI.Handlers {
    public class GlobalExceptionHandler : IExceptionHandler {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
            var (statusCode, message) = exception switch {
                NotFoundException => (HttpStatusCode.NotFound, exception.Message),
                ArgumentException => (HttpStatusCode.BadRequest, exception.Message),
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred")
            };

            httpContext.Response.ContentType = "text/plain";
            httpContext.Response.StatusCode = (int)statusCode;
            await httpContext.Response.WriteAsync(message, cancellationToken);
            return true;
        }
    }
}
