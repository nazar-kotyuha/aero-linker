using AeroLinker.Core.DAL.Enums;

namespace AeroLinker.Core.Common.DTO.Project;

public sealed class ProjectDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}