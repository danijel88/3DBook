using _3DBook.Core.MachineAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _3DBook.Infrastructure.Data.Config;

public class MachineConfiguration : IEntityTypeConfiguration<Machine>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Machine> builder)
    {
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
        builder.Property(p => p.SortCode)
            .IsRequired()
            .HasMaxLength(DataSchemaConstants.DEFAULT_SHORT_NAME_LENGTH);
    }
}