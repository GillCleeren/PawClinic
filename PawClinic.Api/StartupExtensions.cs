using PawClinic.Api.Endpoints;
using PawClinic.Api.Middleware;
using PawClinic.Api.Services;
using PawClinic.Application;
using PawClinic.Application.Contracts;
using PawClinic.Identity;
using PawClinic.Infrastructure;
using PawClinic.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PawClinic.Api
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddOpenApi();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Open", policy =>
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            builder.Services.AddAuthorization();

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseCustomExceptionHandler();
            app.UseCors("Open");
            app.UseAuthorization();

            app.MapOwnerEndpoints();
            app.MapPetEndpoints();
            app.MapAppointmentEndpoints();
            app.MapVetEndpoints();
            app.MapAccountEndpoints();

            return app;
        }

        public static async Task ResetDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetService<PawClinicDbContext>();
                if (context != null && context.Database.IsRelational())
                {
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.MigrateAsync();
                }

                var identityContext = scope.ServiceProvider.GetService<PawClinicIdentityDbContext>();
                if (identityContext != null && identityContext.Database.IsRelational())
                {
                    await identityContext.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                app.Logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
    }
}
