﻿using _3DBook.Core.FolderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _3DBook.Infrastructure.Data.Config;

public class FolderConfiguration : IEntityTypeConfiguration<Folder>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
        builder.Property(p => p.Enter)
            .HasColumnType(DataSchemaConstants.DEFAULT_DECIMAL_TYPE);
        builder.Property(p => p.Exit)
            .HasColumnType(DataSchemaConstants.DEFAULT_DECIMAL_TYPE);
    }
}