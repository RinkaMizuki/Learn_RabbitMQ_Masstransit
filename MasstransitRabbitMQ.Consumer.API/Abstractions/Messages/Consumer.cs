using MassTransit;
using MasstransitRabbitMQ.Contract.Abstractions.Messages;
using MediatR;

namespace MasstransitRabbitMQ.Consumer.API.Abstractions.Messages
{
    public abstract class Consumer<TMessage> : IConsumer<TMessage> where TMessage : class, INotificationEvent
    {
        private readonly IPublisher _publisher;

        protected Consumer(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task Consume(ConsumeContext<TMessage> context)
        {
            await _publisher.Publish(context.Message);
        }
    }
}
