using AeroLinker.Core.Common.DTO.Tag;
using AeroLinker.Core.DAL.Enums;

namespace AeroLinker.Core.Common.DTO.Project;

public sealed class ProjectResponseDto
{
    public int Id { get; set; }
    public bool IsAuthor { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<TagDto>? Tags { get; set; }
}
