import { Component, OnDestroy, OnInit } from '@angular/core';
import { DroneService } from '@core/services/drone.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import * as L from 'leaflet';
import { Subscription } from 'rxjs';

import { DroneTelemetryDto } from 'src/app/models/drone/drone-telemetry-dto';
import { ProjectDroneDto } from 'src/app/models/drone/project-drone-dto';

@Component({
    selector: 'app-map',
    templateUrl: './map.component.html',
    styleUrls: ['./map.component.sass'],
})
export class MapComponent implements OnInit, OnDestroy {
    private map: L.Map | undefined;

    private droneMarkers: L.Marker[] = [];

    private telemetrySubscription: Subscription | undefined;

    private droneSelectionSubscription: Subscription | undefined;

    private droneName: string;

    constructor(private droneService: DroneService, private sharedProjectService: SharedProjectService) {}

    ngOnInit(): void {
        this.initMap();

        // Subscribe to current drone changes
        this.droneSelectionSubscription = this.sharedProjectService.currentDrone$.subscribe((drone) => {
            if (drone) {
                this.connectToDrone(drone);
            } else {
                this.disconnectFromDrone();
            }
        });
    }

    ngOnDestroy(): void {
        if (this.telemetrySubscription) {
            this.telemetrySubscription.unsubscribe();
        }
        if (this.droneSelectionSubscription) {
            this.droneSelectionSubscription.unsubscribe();
        }
        this.disconnectFromDrone();
        this.destroyMap();
    }

    private initMap(): void {
        if (this.map) {
            this.map.remove();
        }
        this.map = L.map('map', {
            center: [50.6198, 26.2514], // Initial coordinates (latitude, longitude)
            zoom: 13,
        });

        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
        }).addTo(this.map);
    }

    private destroyMap(): void {
        if (this.map) {
            this.map.off();
            this.map.remove();
            this.map = undefined;
        }
    }

    private connectToDrone(drone: ProjectDroneDto): void {
        this.disconnectFromDrone();
        this.droneService.connect(drone.droneId);
        this.droneName = drone.droneName;

        if (this.telemetrySubscription) {
            this.telemetrySubscription.unsubscribe();
        }

        this.telemetrySubscription = this.droneService
            .getTelemetryUpdates()
            .subscribe((telemetry: DroneTelemetryDto) => {
                this.updateDronePosition(telemetry);
            });
    }

    private disconnectFromDrone(): void {
        if (this.telemetrySubscription) {
            this.telemetrySubscription.unsubscribe();
        }
        this.droneMarkers.forEach((marker) => this.map!.removeLayer(marker));
        this.droneMarkers = [];
        this.droneService.disconnect();
    }

    private addDroneMarker(lat: number, lng: number): L.Marker {
        const droneIcon = L.icon({
            iconUrl: 'assets/drone.svg',
            iconSize: [10, 10], // size of the icon
            iconAnchor: [25, 25], // point of the icon which will correspond to marker's location
            popupAnchor: [0, -25], // point from which the popup should open relative to the iconAnchor
        });

        const marker = L.marker([lat, lng], {
            icon: droneIcon,
            draggable: true,
        })
            .bindPopup(this.droneName)
            .addTo(this.map!);

        return marker;
    }

    updateDronePosition(telemetry: DroneTelemetryDto): void {
        if (this.droneMarkers.length === 0) {
            const marker = this.addDroneMarker(telemetry.latitude, telemetry.longitude);

            this.droneMarkers.push(marker);
        } else {
            const drone = this.droneMarkers[0];
            const newLatLng = new L.LatLng(telemetry.latitude, telemetry.longitude, telemetry.altitude);

            drone.setLatLng(newLatLng);

            const newIconSize = ((telemetry.altitude + 1) / 99) * 40 + 10;
            const newIcon = L.icon({
                iconUrl: 'assets/drone.svg',
                iconSize: [newIconSize, newIconSize], // size of the icon
                iconAnchor: [25, 25], // point of the icon which will correspond to marker's location
                popupAnchor: [0, -25], // point from which the popup should open relative to the iconAnchor
            });

            drone.setIcon(newIcon);

            drone.bindPopup(`${this.droneName}: (${telemetry.batteryLevel}%)`);
        }
    }
}
