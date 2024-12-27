import { DroneFlightLogDto } from '@shared/components/tree/models/tree-node.model';

export interface ProjectDroneFullDto {
    droneId: string;
    droneName: string;
    projectId: number;
    droneFlights: DroneFlightLogDto[];
}
