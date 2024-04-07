using System.Reflection.Emit;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureWithDDD.Persistence.Configurations;

internal sealed class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable(TableNames.Invoices);

        builder.HasKey(x => x.Id);

        //builder.Property(x => x.CustomerId)
        //       .IsRequired();
        builder
             .Property(x => x.InvoiceAmount)
             .HasColumnType("decimal(18,2)");

        builder
            .Property(x => x.InvoiceDiscount)
            .HasColumnType("decimal(18,2)");

        builder
            .Property(x => x.InvoiceTax)
            .HasColumnType("decimal(18,2)");

        builder
            .Property(x => x.InvoiceTotal)
            .HasColumnType("decimal(18,2)");

        //builder
        //   .HasOne(i => i.Customer)
        //   .WithMany(c => c.Invoices)
        //   .HasForeignKey(i => i.CustomerId)
        //   .OnDelete(DeleteBehavior.Cascade);

    }
}
