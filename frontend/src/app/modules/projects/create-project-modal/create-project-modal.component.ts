import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { ValidationsFn } from '@shared/helpers/validations-fn';
import { takeUntil, tap } from 'rxjs';

import { NewProjectDto } from 'src/app/models/projects/new-project-dto';
import { ProjectDto } from 'src/app/models/projects/project-dto';

@Component({
    selector: 'app-create-project-modal',
    templateUrl: './create-project-modal.component.html',
    styleUrls: ['./create-project-modal.component.sass'],
})
export class CreateProjectModalComponent extends BaseComponent implements OnInit {
    @Output() public projectCreated = new EventEmitter<ProjectDto>();

    public projectForm: FormGroup;

    constructor(
        public dialogRef: MatDialogRef<CreateProjectModalComponent>,
        private fb: FormBuilder,
        private projectService: ProjectService,
        private notificationService: NotificationService,
        private spinner: SpinnerService,
    ) {
        super();
    }

    public ngOnInit() {
        this.createForm();
    }

    public createForm() {
        this.projectForm = this.fb.group({
            projectName: [
                '',
                [Validators.required, Validators.minLength(3), Validators.maxLength(50), ValidationsFn.noCyrillic()],
            ],
        });
    }

    public createProject(): void {
        this.spinner.show();

        const newProject: NewProjectDto = {
            project: {
                name: this.projectForm.value.projectName.trim(),
                description: null,
            },
        };

        this.projectService
            .addProject(newProject)
            .pipe(
                takeUntil(this.unsubscribe$),
                tap(() => this.spinner.hide()),
            )
            .subscribe({
                next: (createdProject: ProjectDto) => {
                    this.dialogRef.close(createdProject);
                    this.notificationService.info('Project created successfully');
                    this.projectCreated.emit(createdProject);
                },
                error: () => {
                    this.notificationService.error('Failed to create project');
                },
            });
    }

    public close(): void {
        this.dialogRef.close();
    }
}
