using AeroLinker.Core.BLL.Interfaces;
using AeroLinker.Core.BLL.Services.Abstract;
using AeroLinker.Core.Common.DTO.ProjectDrone;
using AeroLinker.Core.DAL.Context;
using AeroLinker.Core.DAL.Entities;
using AeroLinker.Shared.Exceptions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AeroLinker.Core.BLL.Services;

public sealed class ProjectDroneService : BaseService, IProjectDroneService
{
    public ProjectDroneService(AeroLinkerCoreContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ICollection<ProjectDroneDto>> GetAllProjectDroneAsync(int projectId)
    {
        return await _context.ProjectDrones
            .Where(p => p.ProjectId == projectId)
            .ProjectTo<ProjectDroneDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<ProjectDroneFullDto> GetProjectDroneAsync(Guid droneId)
    {
        var projectDrone = await _context.ProjectDrones
            .Include(d => d.DroneFlights)
            .FirstOrDefaultAsync(p => p.DroneId == droneId);

        var result = _mapper.Map<ProjectDroneFullDto>(projectDrone);

        return result;
    }

    public async Task<ProjectDroneDto> AddNewProjectDroneAsync(ProjectAddDroneDto dto)
    {
        if (await _context.Projects.FindAsync(dto.ProjectId) is null)
        {
            throw new InvalidProjectException();
        }

        var projectDrone = _mapper.Map<ProjectDrone>(dto);

        projectDrone.DroneId = Guid.NewGuid();

        var droneConnectionString = _mapper.Map<DroneConnectionString>(dto.ConnectionString);

        droneConnectionString.ConnectionId = Guid.NewGuid();

        await _context.DroneConnectionStrings.AddAsync(droneConnectionString);

        projectDrone.ConnectionId = droneConnectionString.ConnectionId;

        var addedProjectDb = (await _context.ProjectDrones.AddAsync(projectDrone)).Entity;

        await _context.SaveChangesAsync();

        return _mapper.Map<ProjectDroneDto>(addedProjectDb);
    }
}