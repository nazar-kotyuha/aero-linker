import { Injectable } from '@angular/core';
import { HttpInternalService } from '@core/services/http-internal.service';

import { DroneTelemetryDto } from 'src/app/models/drone/drone-telemetry-dto';

@Injectable({
    providedIn: 'root',
})
export class FlightLogService {
    private readonly flightLogApiUrl = '/api/FlightLog';

    constructor(private httpService: HttpInternalService) {}

    public getFlightLog(flightLogId: string) {
        return this.httpService.getRequest<DroneTelemetryDto[]>(`${this.flightLogApiUrl}/${flightLogId}`);
    }
}
