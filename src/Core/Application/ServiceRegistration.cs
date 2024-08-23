using Application.Extensions;
using Application.Mappers;
using Domain.Entities;
using DrMadWill.Event.Synchronization.Service;
using DrMadWill.EventBus.Base;
using DrMadWill.EventBus.Base.Abstractions;
using DrMadWill.EventBus.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        if (services == null) throw new AggregateException("service is null");
        services.AddAutoMapper(typeof(GateWayProfile));
        services.AddMediatR(config => { 
            config.RegisterServicesFromAssemblies((typeof(ServiceRegistration)).Assembly,typeof(BaseEntity<>).Assembly);
        });
        services.EventBusHandlers();
        
        services.AddSingleton<IEventBus>(sp =>
        {
            EventBusConfig config = new()
            {
                ConnectionRetryCount = 5,
                SubscriberClientAppName = "GateWay.Service",
                EventBusType = EventBusType.RabbitMQ
            };
            
            return new EventBusRabbitMq(config, sp,new ConnectionFactory
            {
                Uri = new Uri(Configuration.RabbitMqConnectionString)
            });
        });
        services.AddSynchronizationServices();
    }

    private static IServiceCollection EventBusHandlers(this IServiceCollection services)
    {
        //services.AddTransient<BrandChangingEventHandler>();
        return services;
    }

    public static void UsingSubScribes(this IEventBus eventBus)
    { 
        //eventBus.Subscribe<BrandChangingIntegrationEvent, BrandChangingEventHandler>();
    }
}