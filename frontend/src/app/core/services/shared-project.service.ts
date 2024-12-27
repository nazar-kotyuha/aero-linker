import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { ProjectDroneDto } from 'src/app/models/drone/project-drone-dto';

import { ProjectResponseDto } from '../../models/projects/project-response-dto';

@Injectable({
    providedIn: 'root',
})
export class SharedProjectService {
    private project = new BehaviorSubject<ProjectResponseDto | null>(null);

    private currentDrone = new BehaviorSubject<ProjectDroneDto | null>(null);

    get project$() {
        return this.project.asObservable();
    }

    setProject(data: ProjectResponseDto | null) {
        this.project.next(data);
    }

    get currentDrone$() {
        return this.currentDrone.asObservable();
    }

    setCurrentDrone(data: ProjectDroneDto | null) {
        this.currentDrone.next(data);
    }
}
