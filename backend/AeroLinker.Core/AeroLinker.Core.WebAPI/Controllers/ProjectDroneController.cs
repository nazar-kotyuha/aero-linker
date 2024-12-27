using AeroLinker.Core.BLL.Interfaces;
using AeroLinker.Core.Common.DTO.ProjectDrone;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroLinker.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class ProjectDroneController : ControllerBase
{
    private readonly IProjectDroneService _projectDroneService;

    public ProjectDroneController(IProjectDroneService projectDroneService)
    {
        _projectDroneService = projectDroneService;
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDroneDto>> AddNewProjectDroneAsync([FromBody] ProjectAddDroneDto databaseDto)
    {
        return Ok(await _projectDroneService.AddNewProjectDroneAsync(databaseDto));
    }

    [HttpGet("all/{projectId}")]
    public async Task<ActionResult<ICollection<ProjectDroneDto>>> GetAllProjectDronesAsync(int projectId)
    {
        return Ok(await _projectDroneService.GetAllProjectDroneAsync(projectId));
    }

    [HttpGet("{droneId}")]
    public async Task<ActionResult<ProjectDroneFullDto>> GetProjectDroneAsync(Guid droneId)
    {
        return Ok(await _projectDroneService.GetProjectDroneAsync(droneId));
    }
}