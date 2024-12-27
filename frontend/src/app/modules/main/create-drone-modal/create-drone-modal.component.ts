import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { NotificationService } from '@core/services/notification.service';
import { ProjectDroneService } from '@core/services/project-drone.service';

import { ConnectionStringDto } from 'src/app/models/drone/connection-string-dto';
import { DroneConnectorAdapterType } from 'src/app/models/drone/drone-connector-adapter-type';
import { ProjectAddDroneDto } from 'src/app/models/drone/project-add-drone-dto';
import { ProjectDroneDto } from 'src/app/models/drone/project-drone-dto';

@Component({
    selector: 'app-create-drone-modal',
    templateUrl: './create-drone-modal.component.html',
    styleUrls: ['./create-drone-modal.component.sass'],
})
export class CreateDroneModalComponent extends BaseComponent implements OnInit {
    @Output() public addedDrone = new EventEmitter<ProjectDroneDto>();

    public droneForm: FormGroup = new FormGroup({});

    public integratedSecurity = false;

    constructor(
        @Inject(MAT_DIALOG_DATA) private data: any,
        private fb: FormBuilder,
        private droneService: ProjectDroneService,
        private notificationService: NotificationService,
        public dialogRef: MatDialogRef<CreateDroneModalComponent>,
    ) {
        super();
    }

    public ngOnInit() {
        this.initializeForm();
    }

    private initializeForm() {
        this.droneForm = this.fb.group({
            droneName: ['', Validators.required],
            serverName: ['', Validators.required],
            port: [''],
            guid: ['', this.getValidators()],
            adapterType: [''],
        });
    }

    public addDrone() {
        this.saveDb();
    }

    public changeLocalHost() {
        this.droneForm.get('guid')?.setValidators(this.getValidators());
        this.droneForm.get('guid')?.updateValueAndValidity();
    }

    private getValidators() {
        if (this.droneForm.value.localhost) {
            return null;
        }

        return Validators.required;
    }

    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    private saveDb() {
        const connectionStringDto: ConnectionStringDto = {
            serverName: this.droneForm.value.serverName,
            port: this.droneForm.value.port,
            adapterType: parseInt(this.droneForm.value.adapterType, 10) as DroneConnectorAdapterType,
        };

        const drone: ProjectAddDroneDto = {
            projectId: this.data.projectId,
            droneName: this.droneForm.value.droneName,
            connectionString: connectionStringDto,
        };

        this.droneService.addDrone(drone).subscribe({
            next: (droneResponse) => {
                this.notificationService.info('drone was successfully added');
                this.addedDrone.emit(droneResponse);
                this.close();
            },
            error: () => {
                this.notificationService.error('Fail to save drone to db');
            },
        });
    }

    public close() {
        this.dialogRef.close();
    }
}
