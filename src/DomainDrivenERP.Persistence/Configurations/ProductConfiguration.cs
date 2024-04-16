using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Entities.Categories;
using DomainDrivenERP.Domain.Entities.Products;
using DomainDrivenERP.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DomainDrivenERP.Domain.ValueObjects;

namespace DomainDrivenERP.Persistence.Configurations;
internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(TableNames.Products);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.Model)
            .IsRequired();

        builder.Property(x => x.SKU)
            .HasConversion(x => x.Value, v => SKU.Create(v).Value);

        builder.OwnsOne(
                  x => x.Price,
                  price =>
                  {
                      price.Property(p => p.Amount)
                          .HasColumnName("PriceAmount")
                          .HasColumnType("decimal(18,2)");
                      price.Property(p => p.Currency).HasColumnName("PriceCurrency");
                  }
              );

        //builder.HasOne<Category>()
        //        .WithMany()
        //        .HasForeignKey(x => x.CategoryId).IsRequired();
    }
}
