using _3DBook.Core.FolderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _3DBook.Infrastructure.Data.Config;

public class ChildConfiguration : IEntityTypeConfiguration<Child>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Child> builder)
    {
        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
        builder.Property(p => p.Plm)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
        builder.Property(p => p.Thickness)
            .HasColumnType(DataSchemaConstants.DEFAULT_DECIMAL_TYPE);
    }
}