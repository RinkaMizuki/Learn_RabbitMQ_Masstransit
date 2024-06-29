using MassTransit;
using MasstransitRabbitMQ.Contract.Abstractions.Messages;

namespace MasstransitRabbitMQ.Producer.API.DependencyInjection.Options
{
    internal class FancyNameFormatter<TMessage> :
    IMessageEntityNameFormatter<TMessage> where TMessage : class, IMessage
    {
        public string FormatEntityName()
        {
            // seriously, please don't do this, like, ever.
            return typeof(TMessage).Name.ToString();
        }
    }
}
