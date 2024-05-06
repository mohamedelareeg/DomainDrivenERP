using DomainDrivenERP.Domain.Entities.Categories;
using DomainDrivenERP.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainDrivenERP.Persistence.Configurations;
internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(TableNames.Categories);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired();

        builder.HasMany(c => c.Products)
         .WithOne()
         .HasForeignKey(o => o.CategoryId);

    }
}
