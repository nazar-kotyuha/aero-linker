import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { NotificationService } from '@core/services/notification.service';
import { ProjectDroneService } from '@core/services/project-drone.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { CreateDroneModalComponent } from '@modules/main/create-drone-modal/create-drone-modal.component';
import { filter, Subject, Subscription, take, takeUntil } from 'rxjs';

import { ProjectDroneDto } from 'src/app/models/drone/project-drone-dto';
import { ProjectResponseDto } from 'src/app/models/projects/project-response-dto';

@Component({
    selector: 'app-main-header',
    templateUrl: './main-header.component.html',
    styleUrls: ['./main-header.component.sass'],
})
export class MainHeaderComponent implements OnInit, OnDestroy {
    public project: ProjectResponseDto;

    public selectedDroneName: string;

    public droneNames: string[] = [];

    public drones: ProjectDroneDto[] = [];

    private currentDrone: ProjectDroneDto;

    public shouldReset: boolean = false;

    protected unsubscribe$ = new Subject<void>();

    private droneSelectionSubscription: Subscription | undefined;

    public ngOnDestroy() {
        this.unsubscribe$.next();
        this.unsubscribe$.complete();

        if (this.droneSelectionSubscription) {
            this.droneSelectionSubscription.unsubscribe();
        }
    }

    constructor(
        public dialog: MatDialog,
        private sharedProject: SharedProjectService,
        private droneService: ProjectDroneService,
        private notificationService: NotificationService,
    ) {}

    public ngOnInit() {
        this.loadProject();

        this.droneSelectionSubscription = this.sharedProject.currentDrone$.subscribe((drone) => {
            if (drone === null) {
                this.shouldReset = !this.shouldReset;
            }
        });
    }

    public onDroneSelected(value: string) {
        this.selectedDroneName = value;
        this.currentDrone = this.drones!.find((drone) => drone.droneName === this.selectedDroneName)!;

        this.selectDrone(this.currentDrone);
    }

    public openCreateModal(): void {
        const dialogRef = this.dialog.open(CreateDroneModalComponent, {
            width: '700px',
            maxHeight: '90%',
            data: {
                projectId: this.project.id,
            },
            autoFocus: false,
        });

        dialogRef.componentInstance.addedDrone.pipe(takeUntil(this.unsubscribe$)).subscribe({
            next: (addedDrone: ProjectDroneDto) => {
                this.drones.push(addedDrone);
                this.droneNames.push(addedDrone.droneName);
            },
        });
    }

    private loadProject() {
        this.sharedProject.project$
            .pipe(
                filter((project) => project !== null),
                take(1),
            )
            .subscribe({
                next: (project) => {
                    if (project) {
                        this.project = project;
                        this.loadDrones();
                    }
                },
            });
    }

    private loadDrones() {
        this.droneService
            .getAllDrones(this.project.id)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: (drones) => {
                    this.drones = drones;
                    this.droneNames = drones.map((drone) => drone.droneName);
                },
            });
    }

    public selectDrone(drone: ProjectDroneDto) {
        this.currentDrone = drone;

        this.sharedProject.setCurrentDrone(this.currentDrone);
    }
}
