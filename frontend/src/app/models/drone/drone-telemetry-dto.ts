export interface DroneTelemetryDto {
    timestamp: number;
    latitude: number;
    longitude: number;
    altitude: number;
    speed: number;
    batteryLevel: number;
    status: string;
}
