using AeroLinker.Core.DAL.Entities.Common;
using AeroLinker.Core.DAL.Entities.JoinEntities;

namespace AeroLinker.Core.DAL.Entities;

public sealed class User : Entity<int>
{
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PasswordHash { get; set; }
    public string? Salt { get; set; }
    public string? AvatarUrl { get; set; }
    public bool AeroLinkerNotification { get; set; }
    public bool EmailNotification { get; set; }
    public bool IsGoogleAuth { get; set; }

    public ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<Project> OwnProjects { get; set; } = new List<Project>();
}