using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Application.Contract;
using Infrasructure.Implementation;

namespace Infrasructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services
            ,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("GolrangConnection");
            services.AddDbContext<GolrangDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
