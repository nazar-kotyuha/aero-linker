using AeroLinker.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroLinker.Core.DAL.Context.EntityConfigurations;

public sealed class DroneFlightLogConfig : IEntityTypeConfiguration<DroneFlightLog>
{
    public void Configure(EntityTypeBuilder<DroneFlightLog> builder)
    {
        builder.Property(x => x.Guid).IsRequired();

        builder.HasKey(x => x.Guid);

        builder.HasOne(x => x.ProjectDrone)
               .WithMany(x => x.DroneFlights)
               .HasForeignKey(x => x.ProjectDroneId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
    }
}