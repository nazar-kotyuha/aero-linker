import { DroneConnectorAdapterType } from './drone-connector-adapter-type';

export interface ConnectionStringDto {
    serverName: string;
    port: number;
    adapterType: DroneConnectorAdapterType;
}
