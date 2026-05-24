using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PawClinic.Application.Contracts.Infrastructure;
using PawClinic.Application.Models.Mail;
using PawClinic.Infrastructure.Mail;

namespace PawClinic.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            return services;
        }
    }
}
