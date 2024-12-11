using LabirentFethiye.Infastructure.Abstract.Interfaces;
using LabirentFethiye.Infastructure.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace LabirentFethiye.Infastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IMailService, MailService>();
        }
    }
}
