using MassTransit;
using MasstransitRabbitMQ.Contract.Abstractions.IntergrationEvents;
using MasstransitRabbitMQ.Contract.Abstractions.Messages;
using MasstransitRabbitMQ.Consumer.API.DependencyInjection.Options;
using System.Reflection;
using MasstransitRabbitMQ.Consumer.API.MessageBus.Events;
using MasstransitRabbitMQ.Consumer.API.Datas;
using MasstransitRabbitMQ.Consumer.API.Features.Events;

namespace MasstransitRabbitMQ.Consumer.API.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEntityFrameworkCoreConfiguration(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();
            return services;
        }
        public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(config => 
            {
                config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });
            return services;
        }
        public static IServiceCollection AddConfigureMasstransit(this IServiceCollection services, IConfiguration config)
        {
            var masstransitConfiguration = new MasstransitConfiguration();
            config.GetSection(nameof(MasstransitConfiguration)).Bind(masstransitConfiguration);
            services.AddMassTransit(mt =>
            {
                //mt.AddConsumer<SendSmsWhenReceivedSmsEventConsumer>();
                mt.AddConsumers(Assembly.GetExecutingAssembly()); // Add All Comsumer
                mt.UsingRabbitMq((context, bus) => 
                {
                    bus.Host(masstransitConfiguration.Host, masstransitConfiguration.VHost, h=>
                    {
                        h.Username(masstransitConfiguration.UserName);
                        h.Password(masstransitConfiguration.Password);
                    });

                    //Fluent Api Config
                    bus.Publish<IMessage>(p => p.Exclude = true);
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
                    bus.PrefetchCount = 20;
                    //Endpoint Consumer Config
                    bus.ReceiveEndpoint("sms-service", e => 
                    {
                        e.ConcurrentMessageLimit = 10;
                        e.ConfigureConsumer<SendSmsWhenReceivedSmsEventConsumer>(context);
                    });
                    bus.ReceiveEndpoint("email-service", e =>
                    {
                        e.ConcurrentMessageLimit = 10;
                        e.ConfigureConsumer<SendEmailWhenReceivedEmailEventConsumer>(context);
                    });
                });
            });
            return services;
        }
    }
}