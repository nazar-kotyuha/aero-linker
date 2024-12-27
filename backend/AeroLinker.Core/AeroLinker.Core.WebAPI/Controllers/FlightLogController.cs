using AeroLinker.Core.BLL.Services;
using AeroLinker.Shared.DTO.Drone;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroLinker.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class FlightLogController : ControllerBase
{
    private readonly FlightLogService _flightLogService;

    public FlightLogController(FlightLogService flightLogService)
    {
        _flightLogService = flightLogService;
    }

    [HttpGet("{flightLogId}")]
    public async Task<ActionResult<List<DroneTelemetryDto>>> GetFlightLogAsync(string flightLogId)
    {
        return Ok(await _flightLogService.GetStagedAsync(flightLogId));
    }
}