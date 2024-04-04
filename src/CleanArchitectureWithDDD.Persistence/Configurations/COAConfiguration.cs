using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureWithDDD.Persistence.Configurations;
internal sealed class COAConfiguration : IEntityTypeConfiguration<COA>
{
    public void Configure(EntityTypeBuilder<COA> builder)
    {
        builder.ToTable(TableNames.Coas);

        builder.Ignore(c => c.Id);

        builder.HasKey(c => c.HeadCode);

        builder.Property(c => c.HeadCode)
            .IsRequired();

        builder.Property(c => c.HeadName)
            .IsRequired();

        builder.HasMany(c => c.COAs)
            .WithOne(c => c.ParentCOA)
            .HasForeignKey(c => c.ParentHeadCode)
            .IsRequired(false);

        builder.HasMany(c => c.Transactions)
            .WithOne(t => t.COA)
            .HasForeignKey(t => t.COAId)
            .IsRequired(false);
    }
}
