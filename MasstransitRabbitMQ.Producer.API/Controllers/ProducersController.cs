using MassTransit;
using MasstransitRabbitMQ.Contract.Abstractions.IntergrationEvents;
using MasstransitRabbitMQ.Contract.Constants;
using Microsoft.AspNetCore.Mvc;

namespace MasstransitRabbitMQ.Producer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public ProducersController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        [HttpPost("publish-sms-notification")]
        public async Task<IActionResult> PublishSmsNotificationEvent()
        {
            await _publishEndpoint.Publish(new DomainEvent.SmsNotificationEvent()
            {
                Id = Guid.NewGuid(),
                Title = "Sms Notification",
                Description = "Sms Description",
                TimeStamp = DateTime.UtcNow,
                TransactionId = Guid.NewGuid(),
                Type = NotificationType.Sms,
                NumberPhone = "+84867706538"
            });
            return StatusCode(200, new
            {
                message = "Publish sms notification successfully.",
                statusCode = 200,
            });
        }
        [HttpPost("publish-email-notification")]
        public async Task<IActionResult> PublishEmailNotificationEvent()
        {
            await _publishEndpoint.Publish(new DomainEvent.EmailNotificationEvent()
            {
                Id = Guid.NewGuid(),
                Title = "Email Notfication",
                Description = "Email Description",
                TimeStamp = DateTime.UtcNow,
                TransactionId = Guid.NewGuid(),
                Type = NotificationType.Email,
                FromEmail = "nguyenduc09012003@gmail.com",
                ToEmail = "phongdaotao@stu.edu.vn"
            });
            return StatusCode(200, new
            {
                message = "Publish email notification successfully.",
                statusCode = 200
            });
        }
    }
}
