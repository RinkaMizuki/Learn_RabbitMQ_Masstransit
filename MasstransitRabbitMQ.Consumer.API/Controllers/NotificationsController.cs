using MasstransitRabbitMQ.Consumer.API.Datas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MasstransitRabbitMQ.Consumer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public NotificationsController(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        [HttpGet("consume-sms-notification")]
        public async Task<IActionResult> GetAllSmsNotification(CancellationToken cancellationToken)
        {
            var notifications = await _dbContext
                                            .SmsNotificationEvents
                                            .AsNoTracking()
                                            .ToListAsync(cancellationToken);
            return StatusCode(200, new
            {
                message = "Get notification successfully.",
                statusCode = 200,
                notifications,
                length = notifications.Count
            });
        }
        [HttpGet("consume-email-notification")]
        public async Task<IActionResult> GetAllEmailNotfication(CancellationToken cancellationToken)
        {
            var notifications = await _dbContext
                                            .EmailNotificationEvents
                                            .AsNoTracking()
                                            .ToListAsync(cancellationToken);
            return StatusCode(200, new
            {
                message = "Get notification successfully.",
                statusCode = 200,
                notifications,
                length = notifications.Count
            });
        }
    }
}
