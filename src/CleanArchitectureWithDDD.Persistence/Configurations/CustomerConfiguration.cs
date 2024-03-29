using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.ValueObjects;
using CleanArchitectureWithDDD.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Persistence.Configurations
{
    internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(TableNames.Customers);

            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Email)
                .HasConversion(x=>x.Value, v=>Email.Create(v).Value);

            builder.Property(x=>x.FirstName)
                .HasConversion(x=>x.Value , v=> FirstName.Create(v).Value);

            builder.Property(x => x.LastName)
                .HasConversion(x => x.Value, v => LastName.Create(v).Value);

            builder.HasIndex(x=>x.Email).IsUnique();

            builder
                .HasMany(x => x.Invoices)
                .WithOne()
                .HasForeignKey(x => x.CustomerId);
        }
    }
}
