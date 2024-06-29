using MasstransitRabbitMQ.Contract.Abstractions.Messages;

namespace MasstransitRabbitMQ.Contract.Abstractions.IntergrationEvents;

public static class DomainEvent
{
    public class SmsNotificationEvent : INotificationEvent
    {
        public Guid Id { get ; set ; }
        public DateTimeOffset TimeStamp { get ; set ; }
        public string Title { get ; set ; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string NumberPhone { get; set; } = string.Empty;
        public Guid TransactionId { get; set; }
    }
    public class EmailNotificationEvent : INotificationEvent
    {
        public Guid Id { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string ToEmail { get; set; } = string.Empty;
        public Guid TransactionId { get; set; }
    }
}
