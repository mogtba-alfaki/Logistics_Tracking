using System.Text.Json;
using Core.Exceptions;
using Infrastructure.GeoApisProviders;
using Infrastructure.Helpers.AwsS3;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Helpers;

public static class ExceptionMiddlewareExtension {
    public static void HandelGlobalExceptions(this WebApplication app) {
        app.UseExceptionHandler(appBuilder => {
            appBuilder.Run(async context => {
                var exception = context.Features.Get<IExceptionHandlerFeature>().Error;
                switch (exception) {
                    case UnCorrectTripStatusException:
                        await HandleExceptionAsync(context, 
                            "UnCorrectTripStatusException", exception.Message,
                            StatusCodes.Status400BadRequest);
                        break;
                    case RestrictedAreaInPathException:
                        await HandleExceptionAsync(context,
                            "RestrictedAreaInPathException", exception.Message,
                            StatusCodes.Status400BadRequest); 
                        break; 
                    case AwsS3Exception:
                        await HandleExceptionAsync(context, "AwsS3Exception",
                            exception.Message, StatusCodes.Status500InternalServerError);
                        break;
                    case InvalidLoginCredentials:
                        await HandleExceptionAsync(context, "InvalidLoginCredentials",
                            exception.Message, StatusCodes.Status401Unauthorized);
                        break;
                    case UnCorrectTruckStatusException:
                        await HandleExceptionAsync(context, "UnCorrectTruckStatus",
                            exception.Message, StatusCodes.Status400BadRequest);
                        break; 
                    case NotFoundException:
                        await HandleExceptionAsync(context, "NotFound",
                            exception.Message, StatusCodes.Status404NotFound);
                        break;
                    case SpatialDataApisException:
                        await HandleExceptionAsync(context, "SpatialDataApisException",
                            exception.Message, StatusCodes.Status400BadRequest);
                        break;
                    default:
                        var e = new Exception("Internal Server Error");
                        await HandleExceptionAsync(context,
                            "InternalServerError", e.Message,
                            StatusCodes.Status500InternalServerError);
                        break; 
                }
            });
        });
    }
    
    private static async Task HandleExceptionAsync(HttpContext context, string exceptionName, string message, int statusCode) {
        var response = new {
            type = $"www.logistics/tracking/{exceptionName}",
            title = exceptionName,
            status = statusCode,
            detail = message,
            traceId = Guid.NewGuid().ToString()
        };
        context.Response.StatusCode = statusCode; 
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}