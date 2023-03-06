using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.Repositories
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}
