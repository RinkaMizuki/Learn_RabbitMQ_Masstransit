using MasstransitRabbitMQ.Contract.Abstractions.Messages;
using Microsoft.EntityFrameworkCore;
using static MasstransitRabbitMQ.Contract.Abstractions.IntergrationEvents.DomainEvent;

namespace MasstransitRabbitMQ.Consumer.API.Datas
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<SmsNotificationEvent> SmsNotificationEvents {  get; set; }
        public DbSet<EmailNotificationEvent> EmailNotificationEvents { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseInMemoryDatabase("Learn-RabbitMQ-Masstransit");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SmsNotificationEvent>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.TransactionId).IsUnique();
            });
            modelBuilder.Entity<EmailNotificationEvent>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.TransactionId).IsUnique();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
