﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AeroLinker.Core.DAL.Entities;

namespace AeroLinker.Core.DAL.Context.EntityConfigurations;

public sealed class RefreshTokenConfig : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.Property(x => x.Token).IsRequired().HasMaxLength(500);
        builder.Property(x => x.ExpiresAt).IsRequired();
        builder.Property(x => x.CreatedAt)
               .IsRequired()
               .HasDefaultValueSql(AeroLinkerCoreContext.SqlGetDateFunction)
               .ValueGeneratedOnAdd();
        
        builder.HasOne(x => x.User);
    }
}