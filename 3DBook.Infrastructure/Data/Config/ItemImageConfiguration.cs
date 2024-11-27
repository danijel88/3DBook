using _3DBook.Core.ItemAggregate;
using _3DBook.Core.ItemAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _3DBook.Infrastructure.Data.Config;

public class ItemImageConfiguration : IEntityTypeConfiguration<ItemImage>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ItemImage> builder)
    {
        builder.Property(p=>p.Path)
            .IsRequired()
            .HasMaxLength(255);
    }
}