using CleanArchitectureWithDDD.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CleanArchitectureWithDDD.Persistence.Outbox;

namespace CleanArchitectureWithDDD.Persistence.Configurations
{
    internal sealed class OutboxMessageConsumerConfiguration : IEntityTypeConfiguration<OutboxMessageConsumer>
    {
        public void Configure(EntityTypeBuilder<OutboxMessageConsumer> builder)
        {
            builder.ToTable(TableNames.OutboxMessageConsumers);

            //Violation of PRIMARY KEY constraint 'PK_OutboxMessageConsumers'. Cannot insert duplicate key in object 'dbo.OutboxMessageConsumers'
            builder.HasKey(outboxMessageConsumer => new
            {
                outboxMessageConsumer.Id,
                outboxMessageConsumer.Name
            });
        }
    }
}
