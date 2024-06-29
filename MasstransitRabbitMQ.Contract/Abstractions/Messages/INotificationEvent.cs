using MediatR;

namespace MasstransitRabbitMQ.Contract.Abstractions.Messages;

public interface INotificationEvent : IMessage, INotification
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public Guid TransactionId { get; set; }
}
