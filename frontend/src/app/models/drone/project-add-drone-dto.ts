import { ConnectionStringDto } from './connection-string-dto';

export interface ProjectAddDroneDto {
    droneName: string;
    projectId: number;
    connectionString: ConnectionStringDto;
}
