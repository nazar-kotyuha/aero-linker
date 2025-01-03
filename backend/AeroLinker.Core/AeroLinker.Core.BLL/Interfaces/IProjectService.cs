﻿using AeroLinker.Core.Common.DTO.Project;
using AeroLinker.Core.Common.DTO.Users;

namespace AeroLinker.Core.BLL.Interfaces;

public interface IProjectService
{
    Task<ProjectResponseDto> AddProjectAsync(NewProjectDto newProjectDto);
    Task<ProjectResponseDto> UpdateProjectAsync(int projectId, UpdateProjectDto updateProjectDto);
    Task DeleteProjectAsync(int projectId);
    Task<ProjectResponseDto> GetProjectAsync(int projectId);
    Task<ProjectResponseDto> AddUsersToProjectAsync(int projectId, List<UserDto> usersDtos);
    Task<ICollection<UserDto>> GetProjectUsersAsync(int projectId);
    Task<ICollection<ProjectResponseDto>> GetAllUserProjectsAsync();
    Task<bool> RemoveUserFromProjectAsync(int projectId, UserDto userDto);
}