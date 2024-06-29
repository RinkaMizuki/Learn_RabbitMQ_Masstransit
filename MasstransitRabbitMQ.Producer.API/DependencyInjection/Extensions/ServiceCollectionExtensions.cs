using MassTransit;
using MasstransitRabbitMQ.Contract.Abstractions.IntergrationEvents;
using MasstransitRabbitMQ.Contract.Abstractions.Messages;
using MasstransitRabbitMQ.Producer.API.DependencyInjection.Options;
using MediatR;

namespace MasstransitRabbitMQ.Producer.API.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigureMasstransit(this IServiceCollection services, IConfiguration config)
        {
            var masstransitConfiguration = new MasstransitConfiguration();
            config.GetSection(nameof(MasstransitConfiguration)).Bind(masstransitConfiguration);
            services.AddMassTransit(mt =>
            {
                mt.UsingRabbitMq((context, bus) => 
                {
                    bus.Host(masstransitConfiguration.Host, masstransitConfiguration.VHost, h=>
                    {
                        h.Username(masstransitConfiguration.UserName);
                        h.Password(masstransitConfiguration.Password);
                    });
                    bus.Publish<IMessage>(p => p.Exclude = true);
                    bus.Publish<INotification>(p =>  p.Exclude = true);
                    bus.Message<DomainEvent.SmsNotificationEvent>(x => 
                    {
                        x.SetEntityNameFormatter(new FancyNameFormatter<DomainEvent.SmsNotificationEvent>());
                    });
                    bus.Message<DomainEvent.EmailNotificationEvent>(x =>
                    {
                        x.SetEntityNameFormatter(new FancyNameFormatter<DomainEvent.EmailNotificationEvent>());
                    });
                    bus.Message<INotificationEvent>(x =>
                    {
                        x.SetEntityNameFormatter(new FancyNameFormatter<INotificationEvent>());
                    });
                });
            });
            return services;
        }
    }
}
