using Core.Helpers.Email;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolver
{
    public static class ServiceRegistration
    {
        public static void AddCoreService(this IServiceCollection service)
        {
            service.AddScoped<IEmailService, EmailManager>();
        }
    }
}
