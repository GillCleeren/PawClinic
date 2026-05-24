using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PawClinic.Persistence;

namespace PawClinic.Api.IntegrationTests.Base
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(IDbContextOptionsConfiguration<PawClinicDbContext>));
                if (dbContextDescriptor is not null)
                    services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbConnection));
                if (dbConnectionDescriptor is not null)
                    services.Remove(dbConnectionDescriptor);

                services.AddDbContext<PawClinicDbContext>(options =>
                {
                    options.UseInMemoryDatabase("PawClinicDbContextInMemoryTest");
                });
            });
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host = base.CreateHost(builder);

            using var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PawClinicDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

            context.Database.EnsureCreated();

            try
            {
                Utilities.InitializeDbForTests(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the test database.");
            }

            return host;
        }

        public HttpClient GetAnonymousClient() => CreateClient();
    }
}
