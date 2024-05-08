using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddMediatR(conf => conf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        }
    }
}
