using Microsoft.Extensions.DependencyInjection;
using Silkflo.API.Services.ClientApplicationInterfaceSession.Command;
using Silkflo.API.Services.ClientApplicationInterfaceSession.CommandHandler;
using Silkflo.API.Services.Ideas.Queries;
using Silkflo.API.Services.Ideas.QueryHandlers;
using Silkflo.Persistence.Abstractions;
using Silkflo.Persistence.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silkflo.API
{
    public static class ServiceExtention
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateClientApplicationInterfaceSessionCommand).Assembly,
                typeof(CreateClientApplicationInterfaceSessionCommandHandler).Assembly, typeof(GetAllClientIdeasQuery).Assembly,
            typeof(GetAllClientIdeasQueryHandler).Assembly));

            return services;
        }
    }
}
