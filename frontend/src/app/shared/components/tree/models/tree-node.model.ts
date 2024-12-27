export interface DroneFlightLogDto {
    guid: string;
    projectDroneId: string;
    flightLogId: string;
}

export interface TreeNode {
    name: string;
    element?: DroneFlightLogDto;
    selected?: boolean;
}
