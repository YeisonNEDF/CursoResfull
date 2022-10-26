using Core.Interfaces;
using Infraestructure.Service;

namespace WebApi
{
    public static class ServiceExtensions
    {
        public static void AddSharedInfraestruture(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
        }
    }
}
