using AeroLinker.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroLinker.Core.DAL.Context.EntityConfigurations;

public sealed class ProjectDroneConfig : IEntityTypeConfiguration<ProjectDrone>
{
    public void Configure(EntityTypeBuilder<ProjectDrone> builder)
    {
        builder.Property(x => x.DroneName).IsRequired().HasMaxLength(20);
        builder.Property(x => x.DroneId).IsRequired();

        builder.HasKey(x => x.DroneId);

        builder.HasOne(x => x.Project)
               .WithMany(x => x.ProjectDrones)
               .HasForeignKey(x => x.ProjectId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DroneConnectionString)
               .WithOne(x => x.ProjectDrone)
               .HasForeignKey<ProjectDrone>(x => x.ConnectionId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
    }
}