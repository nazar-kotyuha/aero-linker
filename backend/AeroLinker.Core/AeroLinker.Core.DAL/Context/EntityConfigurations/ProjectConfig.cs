﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AeroLinker.Core.DAL.Entities;
using AeroLinker.Core.DAL.Entities.JoinEntities;

namespace AeroLinker.Core.DAL.Context.EntityConfigurations;

public sealed class ProjectConfig : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Description).HasMaxLength(1000);
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.CreatedAt)
               .IsRequired()
               .HasDefaultValueSql(AeroLinkerCoreContext.SqlGetDateFunction)
               .ValueGeneratedOnAdd();
        builder.Property(x => x.UpdatedAt)
               .IsRequired()
               .HasDefaultValueSql(AeroLinkerCoreContext.SqlGetDateFunction)
               .ValueGeneratedOnAddOrUpdate();

        builder.HasMany(x => x.Tags)
               .WithMany(x => x.Projects)
               .UsingEntity<ProjectTag>(
                   l => l.HasOne(x => x.Tag)
                         .WithMany(x => x.ProjectTags)
                         .HasForeignKey(x => x.TagId),
                   r => r.HasOne(x => x.Project)
                         .WithMany(x => x.ProjectTags)
                         .HasForeignKey(x => x.ProjectId));
    }
}