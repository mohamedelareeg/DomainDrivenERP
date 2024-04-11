using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Entities.LineItems;
using CleanArchitectureWithDDD.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CleanArchitectureWithDDD.Domain.Entities.Orders;

namespace CleanArchitectureWithDDD.Persistence.Configurations;
internal sealed class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
{
    public void Configure(EntityTypeBuilder<LineItem> builder)
    {
        builder.ToTable(TableNames.LineItems);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.OwnsOne(
        x => x.UnitPrice,
        price =>
        {
            price.Property(p => p.Amount)
                .HasColumnName("UnitPriceAmount")
                .HasColumnType("decimal(18,2)");
            price.Property(p => p.Currency).HasColumnName("UnitPriceCurrency");
        }
    );

        //builder.HasOne<Order>()
        //  .WithMany()
        //  .HasForeignKey(x => x.OrderId).IsRequired();
    }
}
