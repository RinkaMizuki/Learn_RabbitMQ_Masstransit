using MassTransit;
using MasstransitRabbitMQ.Consumer.API.Datas;
using MasstransitRabbitMQ.Contract.Abstractions.IntergrationEvents;
using MediatR;

namespace MasstransitRabbitMQ.Consumer.API.Features.Events
{
    public class SendSmsEventConsumerHandler : INotificationHandler<DomainEvent.SmsNotificationEvent>
    {
        private readonly ApplicationDbContext _context;
        public SendSmsEventConsumerHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Handle(DomainEvent.SmsNotificationEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                await _context
                            .SmsNotificationEvents
                            .AddAsync(notification, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                Console.WriteLine(notification.TransactionId);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }
    }
}
