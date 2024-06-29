using MasstransitRabbitMQ.Consumer.API.Abstractions.Messages;
using MediatR;
using static MasstransitRabbitMQ.Contract.Abstractions.IntergrationEvents.DomainEvent;

namespace MasstransitRabbitMQ.Consumer.API.MessageBus.Events
{
    public class SendSmsWhenReceivedSmsEventConsumer : Consumer<SmsNotificationEvent>
    {
        public SendSmsWhenReceivedSmsEventConsumer(IPublisher publisher)
            :base(publisher)
        {
            
        }
    }
}
