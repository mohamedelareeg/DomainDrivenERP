using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Entities.Journals;
using DomainDrivenERP.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainDrivenERP.Persistence.Configurations;
internal sealed class JournalConfiguration : IEntityTypeConfiguration<Journal>
{
    public void Configure(EntityTypeBuilder<Journal> builder)
    {
        builder.ToTable(TableNames.Journals);

        builder.HasKey(j => j.Id);

        builder.Property(j => j.Description).HasMaxLength(255);

        builder.HasMany(j => j.Transactions)
               .WithOne(t => t.Journal)
               .HasForeignKey(t => t.JournalId);
    }
}
