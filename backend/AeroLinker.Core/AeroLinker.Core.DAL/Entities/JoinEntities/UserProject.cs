using System.ComponentModel.DataAnnotations;
using AeroLinker.Core.DAL.Entities.Common;
using AeroLinker.Core.DAL.Enums;

namespace AeroLinker.Core.DAL.Entities.JoinEntities;

public sealed class UserProject : Entity<int>
{
    [Required]
    public UserRole UserRole { get; set; }

    public int ProjectId { get; set; }
    public int UserId { get; set; }
    public Project Project { get; set; } = null!;
    public User User { get; set; } = null!;
}