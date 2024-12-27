import { Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { DroneService } from '@core/services/drone.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { Subscription } from 'rxjs';

import { Option } from 'src/app/models/drone/option';

@Component({
    selector: 'app-tree',
    templateUrl: './tree.component.html',
    styleUrls: ['./tree.component.sass'],
})
export class TreeComponent implements OnInit, OnDestroy {
    @Input() public asCheckList: boolean = false;

    @Input() public height: string = '100%';

    private droneSelectionSubscription: Subscription | undefined;

    private frameStreamSubscription: Subscription | undefined;

    private intervalId: any;

    private videoTimeoutId: any;

    speed: number = 1;

    altitude: number = 5;

    private step: number = 0.001;

    public noDroneMessage: string | null = null;

    public noVideoMessage: string | null = null;

    private emptyVideoSource: string =
        'data:image/jpeg;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkqPxfDwAD8gH5r00RfgAAAABJRU5ErkJggg==';

    @ViewChild('framePlayer', { static: true }) framePlayer: ElementRef<HTMLImageElement>;

    constructor(private droneService: DroneService, private sharedService: SharedProjectService) {}

    ngOnInit(): void {
        this.frameStreamSubscription = this.droneService.getFrameUpdates().subscribe((base64Image: string) => {
            this.noVideoMessage = null;

            this.framePlayer.nativeElement.src = `data:image/jpeg;base64,${base64Image}`;
            if (this.videoTimeoutId) {
                clearTimeout(this.videoTimeoutId);
            }
            // Set a timeout to show the no video message if no new frame is received within 1 second
            this.videoTimeoutId = setTimeout(() => {
                this.framePlayer.nativeElement.src = this.emptyVideoSource;
                this.noVideoMessage = 'No video source available';
            }, 5000);
        });

        this.droneSelectionSubscription = this.sharedService.currentDrone$.subscribe((drone) => {
            if (drone) {
                this.speed = 1;
                this.step = this.speed / 100000;
                this.altitude = 5;
                this.noDroneMessage = null;
            } else {
                if (this.videoTimeoutId) {
                    clearTimeout(this.videoTimeoutId);
                }

                this.framePlayer.nativeElement.src = this.emptyVideoSource;
                this.noDroneMessage = 'No drone connected';
            }
        });
    }

    ngOnDestroy(): void {
        if (this.droneSelectionSubscription) {
            this.droneSelectionSubscription.unsubscribe();
        }
        if (this.frameStreamSubscription) {
            this.frameStreamSubscription.unsubscribe();
        }
        if (this.videoTimeoutId) {
            clearTimeout(this.videoTimeoutId);
        }
        this.droneService.disconnect();
    }

    startMoving(event: MouseEvent, option: Option): void {
        event.preventDefault();
        event.stopPropagation();
        this.intervalId = setInterval(() => {
            this.moveDrone(option);
        }, 200); // Adjust the interval as needed
    }

    stopMoving(): void {
        clearInterval(this.intervalId);
    }

    moveDrone(option: Option, currentStep: number = this.step): void {
        this.droneService.sendCommand({ option, step: currentStep }); // max 0.001 - min 0.00001
    }

    moveDroneOnClick(event: MouseEvent, option: Option): void {
        event.preventDefault();
        event.stopPropagation();
        this.moveDrone(option);
    }

    updateSpeed(event: Event): void {
        const inputElement = event.target as HTMLInputElement;
        const isAccelerating = +inputElement.value > this.speed;

        this.speed = +inputElement.value;
        const valPercent = (this.speed / +inputElement.max) * 100;

        this.moveDrone(isAccelerating ? Option.Accelerate : Option.Decelerate, this.speed);
        this.step = this.speed / 100000;
        inputElement.style.background = `linear-gradient(to right, #3264fe ${valPercent}%, #d5d5d5 ${valPercent}%)`;
    }

    updateAltitude(event: Event): void {
        const inputElement = event.target as HTMLInputElement;
        const isAscending = +inputElement.value > this.altitude;

        this.altitude = +inputElement.value;
        const valPercent = (this.altitude / +inputElement.max) * 100;

        this.moveDrone(isAscending ? Option.Ascend : Option.Descend, this.altitude);
        inputElement.style.background = `linear-gradient(to right, #3264fe ${valPercent}%, #d5d5d5 ${valPercent}%)`;
    }

    public disconnectAndSaveLog() {
        if (this.videoTimeoutId) {
            clearTimeout(this.videoTimeoutId);
        }

        this.framePlayer.nativeElement.src = this.emptyVideoSource;
        this.noDroneMessage = 'No drone connected';
        this.droneService.disconnect();
    }

    Option = Option; // Expose the enum to the template
}
