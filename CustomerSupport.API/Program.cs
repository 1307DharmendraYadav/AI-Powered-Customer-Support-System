using CustomerSupport.Infrastructure.Extensions;
using CustomerSupport.API.Extensions;
using CustomerSupport.Application.Extensions;

namespace CustomerSupport.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var connectionString =
                builder.Configuration.GetConnectionString("CustomerSupportDBConnection")
                ?? throw new InvalidOperationException("CustomerSupportDBConnection is not configured.");

            builder.Services.AddInfrastructureServices(connectionString);

            // Register the application's global exception handler.
            builder.Services.AddGlobalExceptionHandling();

            // Register Application Services (Chapter 1.6).
            builder.Services.AddApplicationServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            // Add the global exception handling middleware to the HTTP request pipeline.
            app.UseExceptionHandler();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}