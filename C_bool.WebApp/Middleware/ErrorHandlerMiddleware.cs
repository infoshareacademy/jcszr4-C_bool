using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using C_bool.WebApp.Helpers;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace C_bool.WebApp.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<ErrorHandlerMiddleware>();
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
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
                Log.Error("Middleware catched exception: {0}", error.Message);
                await response.WriteAsync(result);
            }
        }
    }
}
