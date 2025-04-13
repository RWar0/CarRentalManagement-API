using Microsoft.EntityFrameworkCore;
using CarRentalManagementAPI.Models.Contexts;

namespace CarRentalManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adding CORS - Allowing working with all applications
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllApps", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Connecting to database
            builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure CORS
            app.UseCors("AllowAllApps");

            // Configure ApiKey
            app.Use(async (context, next) =>
            {
                const string ApiKeyHeaderName = "x-api-key";
                var expectedApiKey = builder.Configuration["ApiSettings:ApiKey"];

                if (string.IsNullOrEmpty(expectedApiKey))
                {
                    context.Response.StatusCode = 500; // Internal Server Error
                    await context.Response.WriteAsync("API Key configuration is missing");
                    return;
                }

                if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
                {
                    context.Response.StatusCode = 401; // Unauthorized
                    await context.Response.WriteAsync("API Key is missing");
                    return;
                }

                if (!expectedApiKey.Equals(extractedApiKey, StringComparison.Ordinal))
                {
                    context.Response.StatusCode = 403; // Forbidden
                    await context.Response.WriteAsync("Invalid API Key");
                    return;
                }

                await next();
            });

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
