using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AeroLinker.Core.DAL.Entities;
using AeroLinker.Core.DAL.Entities.JoinEntities;

namespace AeroLinker.Core.DAL.Context.EntityConfigurations;

public sealed class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasAlternateKey(x => x.Email);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x => x.Username).IsUnique();
        builder.Property(x => x.Username).IsRequired().HasMaxLength(25);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(25);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(25);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
        builder.Property(x => x.PasswordHash).HasMaxLength(100);
        builder.Property(x => x.Salt).HasMaxLength(100);
        builder.Property(x => x.AvatarUrl).HasMaxLength(500);
        builder.Property(x => x.IsGoogleAuth).IsRequired();

        builder.HasMany(x => x.OwnProjects)
               .WithOne(x => x.Author)
               .HasForeignKey(x => x.CreatedBy)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Projects)
               .WithMany(x => x.Users)
               .UsingEntity<UserProject>(
                      l => l.HasOne(x => x.Project)
                            .WithMany(x => x.UserProjects)
                            .HasForeignKey(x => x.ProjectId),
                      r => r.HasOne(x => x.User)
                            .WithMany(x => x.UserProjects)
                            .HasForeignKey(x => x.UserId));
    }
}