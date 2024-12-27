using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AeroLinker.Core.DAL.Entities;

namespace AeroLinker.Core.DAL.Context.EntityConfigurations;

public sealed class TagConfig : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
    }
}