import { Component, OnDestroy, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from '@core/base/base.component';
import { EventService } from '@core/services/event.service';
import { NotificationService } from '@core/services/notification.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { takeUntil } from 'rxjs';

import { ProjectDroneDto } from 'src/app/models/drone/project-drone-dto';

import { ProjectResponseDto } from '../../../../../models/projects/project-response-dto';

@Component({
    selector: 'app-navbar-header',
    templateUrl: './navbar-header.component.html',
    styleUrls: ['./navbar-header.component.sass'],
})
export class NavbarHeaderComponent extends BaseComponent implements OnInit, OnDestroy {
    @ViewChild('modalContent') modalContent: TemplateRef<any>;

    public currentControlsGuId: string;

    public currentBranchId: number;

    public isSettingsEnabled: boolean = false;

    public currentProject: ProjectResponseDto = {} as ProjectResponseDto;

    public lastCommitId: number;

    public selectedDrone: ProjectDroneDto;

    public navLinks: { path: string; displayName: string }[] = [
        { displayName: 'Controls', path: './controls' },
        // { displayName: 'PRs', path: './pull-requests' },
        // { displayName: 'Branches', path: './branches' },
        // { displayName: 'Scripts', path: './scripts' },
        { displayName: 'LogExplorer', path: './log-explorer' },
        { displayName: 'Settings', path: './settings' },
    ];

    constructor(
        public dialog: MatDialog,
        private route: ActivatedRoute,
        private sharedProject: SharedProjectService,
        private notificationService: NotificationService,
        private spinner: SpinnerService,
        private eventService: EventService,
    ) {
        super();
    }

    public ngOnInit(): void {
        this.route.params.subscribe((params) => {
            this.currentProject.id = params['id'];
        });

        this.sharedProject.project$.pipe(takeUntil(this.unsubscribe$)).subscribe({
            next: (project) => {
                if (project) {
                    this.isSettingsEnabled = project.isAuthor;
                    this.currentProject = project;
                }
            },
        });
        this.getCurrentDatabase();
    }

    // public onBranchSelected(value: BranchDto) {
    //     this.selectedBranch = value;
    //     this.branchService.selectBranch(this.currentProject.id, value.id);
    // }

    // public openBranchModal() {
    //     const dialogRef = this.dialog.open(CreateBranchModalComponent, {
    //         width: '500px',
    //         data: { projectId: this.currentProject.id, branches: this.branches },
    //     });

    //     dialogRef.componentInstance.branchCreated.subscribe((branch) => {
    //         this.onBranchSelected(branch);
    //         this.branches.push(branch);
    //     });
    // }

    // public getCurrentBranch() {
    //     this.currentBranchId = this.branchService.getCurrentBranch(this.currentProject.id);
    //     const currentBranch = this.branches.find((x) => x.id === this.currentBranchId);

    //     return currentBranch ? this.branches.indexOf(currentBranch) : 0;
    // }

    // public filterBranch(item: BranchDto, value: string) {
    //     return item.name.includes(value);
    // }

    public getCurrentDatabase() {
        this.sharedProject.currentDrone$.pipe(takeUntil(this.unsubscribe$)).subscribe({
            next: (currentDrone) => {
                this.selectedDrone = currentDrone!;
            },
        });
    }

    public loadControls() {
        if (!this.selectedDrone) {
            this.notificationService.error('No database currently selected');

            return;
        }

        this.spinner.show();
    }
}
