namespace MasstransitRabbitMQ.Contract.Abstractions.Messages;

public interface IMessage
{
    public Guid Id { get; set; }
    public DateTimeOffset TimeStamp { get; set; }
}
