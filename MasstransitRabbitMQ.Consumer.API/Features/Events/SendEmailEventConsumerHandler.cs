using MassTransit;
using MasstransitRabbitMQ.Consumer.API.Datas;
using MasstransitRabbitMQ.Contract.Abstractions.IntergrationEvents;
using MediatR;

namespace MasstransitRabbitMQ.Consumer.API.Features.Events
{
    public class SendEmailEventConsumerHandler : INotificationHandler<DomainEvent.EmailNotificationEvent>
    {
        private readonly ApplicationDbContext _context;
        public SendEmailEventConsumerHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Handle(DomainEvent.EmailNotificationEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                await _context
                            .EmailNotificationEvents
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
