import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FlightLogService } from '@core/services/flight-log.service';
import { ProjectDroneService } from '@core/services/project-drone.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { DroneFlightLogDto, TreeNode } from '@shared/components/tree/models/tree-node.model';
import { Subject, takeUntil } from 'rxjs';

import { ProjectDroneFullDto } from 'src/app/models/drone/project-drone-full-dto';

import { LogMapComponent } from '../log-map/log-map.component'; // Adjust the import based on your actual path

@Component({
    selector: 'app-log-explorer',
    templateUrl: './log-explorer.component.html',
    styleUrls: ['./log-explorer.component.sass'],
})
export class LogExplorerComponent implements OnInit, OnDestroy {
    public items: TreeNode[];

    public selectedItem: TreeNode | null = null;

    public form: FormGroup;

    public currentChangesGuid: string;

    public projectDroneFullDto: ProjectDroneFullDto;

    @ViewChild(LogMapComponent) private logMapComponent: LogMapComponent;

    protected unsubscribe$ = new Subject<void>();

    public ngOnDestroy() {
        this.unsubscribe$.next();
        this.unsubscribe$.complete();

        this.sharedProjectService.setCurrentDrone(null);
    }

    constructor(
        private projectDroneService: ProjectDroneService,
        private sharedProjectService: SharedProjectService,
        private flightLogService: FlightLogService,
    ) {}

    public ngOnInit(): void {
        this.sharedProjectService.currentDrone$.subscribe((drone) => {
            if (drone) {
                this.projectDroneService
                    .getDrone(drone.droneId)
                    .pipe(takeUntil(this.unsubscribe$))
                    .subscribe({
                        next: (droneFull) => {
                            this.projectDroneFullDto = droneFull;
                            this.items = this.mapDroneFlightLogsToTreeNodes(droneFull.droneFlights);
                        },
                        error: (err) => {
                            console.error('Error fetching drone data:', err);
                            this.items = [];
                        },
                    });
            } else {
                this.items = [];
            }
        });
    }

    public selectionChanged(selectedOne: TreeNode): void {
        this.selectedItem = selectedOne;
        if (this.selectedItem && this.selectedItem.element) {
            this.flightLogService.getFlightLog(this.selectedItem.element.flightLogId).subscribe({
                next: (telemetryData) => {
                    this.logMapComponent.updateFlightPath(telemetryData);
                },
                error: (err) => {
                    console.error('Error fetching flight log:', err);
                },
            });
        }
    }

    private mapDroneFlightLogsToTreeNodes(flightLogs: DroneFlightLogDto[]): TreeNode[] {
        return flightLogs.map((log) => {
            const [, timestampStr] = log.flightLogId.split('@');
            const timestamp = parseInt(timestampStr, 10);
            const date = new Date(timestamp);

            const formattedDate = date.toLocaleString();

            return {
                name: formattedDate,
                element: log,
                selected: false,
            };
        });
    }
}
