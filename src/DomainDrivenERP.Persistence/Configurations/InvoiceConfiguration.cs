﻿using System.Reflection.Emit;
using DomainDrivenERP.Domain.Entities.Invoices;
using DomainDrivenERP.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainDrivenERP.Persistence.Configurations;

internal sealed class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable(TableNames.Invoices);

        builder.HasKey(x => x.Id);

        // Sets up a query filter to ensure only non-cancelled invoices are retrieved.
        builder.HasQueryFilter(x => !x.Cancelled);

        // builder.Property(x => x.CustomerId)
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

        // builder
        //   .HasOne(i => i.Customer)
        //   .WithMany(c => c.Invoices)
        //   .HasForeignKey(i => i.CustomerId)
        //   .OnDelete(DeleteBehavior.Cascade);

    }
}
