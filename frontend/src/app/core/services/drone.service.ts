import { Injectable } from '@angular/core';
import { SharedProjectService } from '@core/services/shared-project.service';
import { environment } from '@env/environment';
import { Observable, Subject } from 'rxjs';
import { WebSocketSubject } from 'rxjs/webSocket';

import { Command } from 'src/app/models/drone/command';
import { DroneTelemetryDto } from 'src/app/models/drone/drone-telemetry-dto';

@Injectable({
    providedIn: 'root',
})
export class DroneService {
    public baseUrl: string = environment.droneUrl;

    private socket$: WebSocketSubject<any> | null = null;

    private telemetrySubject: Subject<DroneTelemetryDto> = new Subject<DroneTelemetryDto>();

    private frameSubject: Subject<string> = new Subject<string>();

    private currentFrameUrl: string | null = null;

    constructor(private sharedProjectService: SharedProjectService) {}

    connect(connectionId: string): void {
        this.socket$ = new WebSocketSubject({
            url: `${this.baseUrl}/${connectionId}`,
            deserializer: (msg) => msg.data,
        });

        this.socket$.subscribe(
            (msg) => {
                if (typeof msg === 'string') {
                    try {
                        const data: DroneTelemetryDto = JSON.parse(msg);

                        this.telemetrySubject.next(data);
                    } catch (e) {
                        this.frameSubject.next(msg);
                    }
                } else {
                    this.telemetrySubject.next(msg);
                }
            },
            (err) => {
                console.error(`Disconnected !${err}`);
                this.handleDisconnect();
            },
            () => console.warn('Completed!'),
        );
    }

    disconnect(): void {
        if (this.socket$) {
            this.socket$.complete();
            this.socket$ = null;

            this.sharedProjectService.setCurrentDrone(null);
        }

        if (this.currentFrameUrl) {
            URL.revokeObjectURL(this.currentFrameUrl);
            this.currentFrameUrl = null;
        }
    }

    sendCommand(command: Command): void {
        if (this.socket$) {
            this.socket$.next(command);
        }
    }

    getTelemetryUpdates(): Observable<any> {
        return this.telemetrySubject.asObservable();
    }

    getFrameUpdates(): Observable<string> {
        return this.frameSubject.asObservable();
    }

    checkConnection(): boolean {
        return this.socket$ !== null;
    }

    handleDisconnect(): void {
        this.sharedProjectService.setCurrentDrone(null);
    }
}
