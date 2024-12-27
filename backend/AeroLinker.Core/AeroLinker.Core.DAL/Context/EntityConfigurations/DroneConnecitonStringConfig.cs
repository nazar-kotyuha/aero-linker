using AeroLinker.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroLinker.Core.DAL.Context.EntityConfigurations;

public sealed class DroneConnectionStringConfig : IEntityTypeConfiguration<DroneConnectionString>
{
    public void Configure(EntityTypeBuilder<DroneConnectionString> builder)
    {
        builder.Property(x => x.ServerName).IsRequired().HasMaxLength(40);
        builder.Property(x => x.Port).IsRequired();

        builder.HasKey(x => x.ConnectionId);
    }
}