using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using C_bool.WebApp.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;

namespace C_bool.WebApp.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private static ILogger<ErrorHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                _logger.LogError("Middleware catched exception: {exceptionMessage}", error.Message);
                await response.WriteAsync(result);
            }
        }
    }
}
