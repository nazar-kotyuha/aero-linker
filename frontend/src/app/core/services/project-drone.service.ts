import { Injectable } from '@angular/core';
import { HttpInternalService } from '@core/services/http-internal.service';

import { ProjectAddDroneDto } from 'src/app/models/drone/project-add-drone-dto';
import { ProjectDroneDto } from 'src/app/models/drone/project-drone-dto';
import { ProjectDroneFullDto } from 'src/app/models/drone/project-drone-full-dto';

@Injectable({
    providedIn: 'root',
})
export class ProjectDroneService {
    private readonly droneApiUrl = '/api/ProjectDrone';

    constructor(private httpService: HttpInternalService) {}

    public addDrone(newDrone: ProjectAddDroneDto) {
        return this.httpService.postRequest<ProjectDroneDto>(this.droneApiUrl, newDrone);
    }

    public getAllDrones(projectId: number) {
        return this.httpService.getRequest<ProjectDroneDto[]>(`${this.droneApiUrl}/all/${projectId}`);
    }

    public getDrone(droneId: string) {
        return this.httpService.getRequest<ProjectDroneFullDto>(`${this.droneApiUrl}/${droneId}`);
    }
}
