using Application.Contract;
using Application.Utils;
using Domain.ConfigModel;

namespace Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<JwtConfig>(configuration.GetRequiredSection("Jwt"));

            services.AddScoped<IJwtUtils, JwtUtils>();
            return services;
        }
    }
}
