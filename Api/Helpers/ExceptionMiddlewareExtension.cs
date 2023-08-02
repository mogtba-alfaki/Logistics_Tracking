using System.Text.Json;
using Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Helpers;

public static class ExceptionMiddlewareExtension {
    public static void HandelGlobalExceptions(this WebApplication app) {
        app.UseExceptionHandler(appBuilder => {
            appBuilder.Run(async context => {
                var exception = context.Features.Get<IExceptionHandlerFeature>().Error;
                switch (exception) {
                    case UnCorrectTripStatusException:
                        await HandleCoreLayerExceptionAsync(context, 
                            "UnCorrectTripStatusException", exception.Message,
                            StatusCodes.Status400BadRequest);
                        break;
                    case RestrictedAreaInPathException:
                        await HandleCoreLayerExceptionAsync(context,
                            "RestrictedAreaInPathException", exception.Message,
                            StatusCodes.Status400BadRequest); 
                        break;
                    default:
                        var e = new Exception("Internal Server Error");
                        await HandleCoreLayerExceptionAsync(context,
                            "InternalServerError", e.Message,
                            StatusCodes.Status500InternalServerError);
                        break; 
                }
            });
        });
    }
    
    private static async Task HandleCoreLayerExceptionAsync(HttpContext context, string exceptionName, string message, int statusCode) {
        var response = new {
            type = $"www.logistics/tracking/{exceptionName}",
            title = exceptionName,
            status = statusCode,
            detail = message,
            traceId = Guid.NewGuid().ToString()
        };
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}