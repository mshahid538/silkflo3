using Microsoft.Extensions.DependencyInjection;
using Silkflo.Persistence.Abstractions;
using Silkflo.Persistence.DbConnection;

namespace Silkflo.Persistence
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistentServices(this IServiceCollection services)
        {
            services.AddSingleton<IDatabaseConnection, DatabaseConnection>();

            return services;
        }
    }
}