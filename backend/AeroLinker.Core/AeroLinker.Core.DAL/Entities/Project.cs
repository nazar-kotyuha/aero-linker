using AeroLinker.Core.DAL.Entities.Common.AuditEntity;
using AeroLinker.Core.DAL.Entities.JoinEntities;

namespace AeroLinker.Core.DAL.Entities;

public sealed class Project : AuditEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime UpdatedAt { get; set; }
    public User Author { get; set; } = null!;
    public ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public ICollection<ProjectTag> ProjectTags { get; set; } = new List<ProjectTag>();
    public ICollection<ProjectDrone> ProjectDrones { get; set; } = new List<ProjectDrone>();
}