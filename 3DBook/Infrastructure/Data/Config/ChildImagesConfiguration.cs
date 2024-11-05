using _3DBook.Core.FolderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _3DBook.Infrastructure.Data.Config;

public class ChildImagesConfiguration : IEntityTypeConfiguration<ChildImage>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ChildImage> builder)
    {
        builder.Property(p => p.Path)
            .IsRequired()
            .HasMaxLength(255);
    }
}