using Application.Extensions;
using DrMadWill.Layers.Repository;
using DrMadWill.Layers.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories.CQRS;
using Persistence.Services.Sys;

namespace Persistence;

public static class ServiceRegistration
{

    public static void AddPersistenceServices(this IServiceCollection services)
    {
        if (services == null) throw new AggregateException("service is null");
        services.AddDbContext<GateWayContext>(options => options.UseSqlServer(Configuration.ConnectionString));
        
        #region Repositories

        services.LayerRepositoriesRegister<GateWayUnitOfWork, GateWayQueryRepositories>();
        #endregion

        #region Services
        services.LayerServicesRegister<GateWayServiceManager>();
        #endregion

    }

}