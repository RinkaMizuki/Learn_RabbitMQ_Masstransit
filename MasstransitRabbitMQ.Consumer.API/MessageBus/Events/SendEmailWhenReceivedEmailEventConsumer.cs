using MasstransitRabbitMQ.Consumer.API.Abstractions.Messages;
using MasstransitRabbitMQ.Contract.Abstractions.IntergrationEvents;
using MediatR;

namespace MasstransitRabbitMQ.Consumer.API.MessageBus.Events
{
    public class SendEmailWhenReceivedEmailEventConsumer : Consumer<DomainEvent.EmailNotificationEvent>
    {
        public SendEmailWhenReceivedEmailEventConsumer(IPublisher publisher) : base(publisher)
        {
        }
    }
}
