using CustomerSupport.API.ExceptionHandling;

namespace CustomerSupport.API.Extensions
{
    public static class ExceptionHandlingExtensions
    {
        // Extension method for IServiceCollection.
        // This allows us to register exception handling in Program.cs
        // using a clean statement such as:
        // builder.Services.AddGlobalExceptionHandling();
        public static IServiceCollection AddGlobalExceptionHandling(this IServiceCollection services)
        {
            // Register GlobalExceptionHandler as the application's centralized exception handler.
            // Whenever an unhandled exception occurs during request processing,
            // ASP.NET Core can pass that exception to GlobalExceptionHandler.
            services.AddExceptionHandler<GlobalExceptionHandler>();

            // Required by UseExceptionHandler() so the middleware has a
            // ProblemDetails service to fall back on / format responses with.
            services.AddProblemDetails();

            // Return IServiceCollection so that additional services
            // can be registered using method chaining.
            return services;
        }
    }
}
