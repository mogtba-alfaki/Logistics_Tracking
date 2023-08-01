using Microsoft.AspNetCore.Diagnostics;

namespace Api.Helpers;

public static class ExceptionMiddlewareExtension {
    public static void HandelGlobalExceptions(this WebApplication app) {
        app.UseExceptionHandler(error => {
            error.Run(context => {
                var error = context.Features.Get<IExceptionHandlerFeature>().Error;
                // handle Exceptions ..  

                return null;
            });
        });
    }
}