namespace MasstransitRabbitMQ.Consumer.API.DependencyInjection.Options
{
    public class MasstransitConfiguration
    {
        public string Host { get; set; } = string.Empty;
        public string VHost {  get; set; } = string.Empty;
        public string UserName {  get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ExchangeName {  get; set; } = string.Empty;
        public string ExchangeType { get; set; } = string.Empty;    
        public string SmsQueueName {  get; set; } = string.Empty;
        public string EmailQueueName { get;set; } = string.Empty;
    }
}
