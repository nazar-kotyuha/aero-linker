using AeroLinker.Core.DAL.Entities;
using AeroLinker.Core.DAL.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;

namespace AeroLinker.Core.DAL.Context;

public class AeroLinkerCoreContext : DbContext
{
    public const string SqlGetDateFunction = "getutcdate()";
    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<ProjectTag> ProjectTags => Set<ProjectTag>();
    public DbSet<UserProject> UserProjects => Set<UserProject>();
    public DbSet<ProjectDrone> ProjectDrones => Set<ProjectDrone>();
    public DbSet<DroneConnectionString> DroneConnectionStrings => Set<DroneConnectionString>();
    public DbSet<DroneFlightLog> DroneFlightLogs => Set<DroneFlightLog>();

    public AeroLinkerCoreContext(DbContextOptions<AeroLinkerCoreContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Configure();
    }
}
