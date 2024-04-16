using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Entities.LineItems;
using DomainDrivenERP.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DomainDrivenERP.Domain.Entities.Orders;

namespace DomainDrivenERP.Persistence.Configurations;
internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(TableNames.Orders);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CustomerId)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.HasMany(c => c.LineItems)
            .WithOne()
            .HasForeignKey(o => o.OrderId);
    }
}
