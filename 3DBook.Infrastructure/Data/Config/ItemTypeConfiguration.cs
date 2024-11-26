using _3DBook.Core.ItemAggregate;
using _3DBook.Core.ItemAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _3DBook.Infrastructure.Data.Config;

public class ItemTypeConfiguration : IEntityTypeConfiguration<ItemType>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ItemType> builder)
    {
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
    }
}