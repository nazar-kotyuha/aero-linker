import { Component, OnDestroy, OnInit } from '@angular/core';
import * as L from 'leaflet';

import { DroneTelemetryDto } from 'src/app/models/drone/drone-telemetry-dto'; // Ensure this path is correct

@Component({
    selector: 'app-log-map',
    templateUrl: './log-map.component.html',
    styleUrls: ['./log-map.component.sass'],
})
export class LogMapComponent implements OnInit, OnDestroy {
    private map: L.Map | undefined;

    private flightPath: L.Polyline | undefined;

    private startMarker: L.Marker | undefined;

    private finishMarker: L.Marker | undefined;

    private droneMarkers: L.Marker[] = [];

    ngOnInit(): void {
        this.initMap();
    }

    ngOnDestroy(): void {
        this.destroyMap();
    }

    private initMap(): void {
        if (this.map) {
            this.map.remove();
        }
        this.map = L.map('log-map', {
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

    public updateFlightPath(telemetryData: DroneTelemetryDto[]): void {
        if (this.flightPath) {
            this.flightPath.remove();
        }
        if (this.startMarker) {
            this.startMarker.remove();
        }
        if (this.finishMarker) {
            this.finishMarker.remove();
        }
        this.droneMarkers.forEach((marker) => marker.remove());
        this.droneMarkers = [];

        const latlngs = telemetryData.map((data) => [data.latitude, data.longitude] as [number, number]);

        this.flightPath = L.polyline(latlngs, { color: 'blue' }).addTo(this.map!);
        this.map!.fitBounds(this.flightPath.getBounds());

        // Add start and finish markers
        const startLatLng = latlngs[0];
        const finishLatLng = latlngs[latlngs.length - 1];

        const startIcon = L.divIcon({
            className: 'start-icon',
            html: '<div style="background-color: green; width: 24px; height: 24px; border-radius: 50%;"></div>',
            iconSize: [10, 10],
            iconAnchor: [12, 24],
            popupAnchor: [0, -24],
        });

        const finishIcon = L.divIcon({
            className: 'finish-icon',
            html: '<div style="background-color: red; width: 24px; height: 24px; border-radius: 50%;"></div>',
            iconSize: [10, 10],
            iconAnchor: [12, 24],
            popupAnchor: [0, -24],
        });

        this.startMarker = L.marker(startLatLng, { icon: startIcon }).addTo(this.map!).bindPopup('Start Point');
        this.finishMarker = L.marker(finishLatLng, { icon: finishIcon }).addTo(this.map!).bindPopup('Finish Point');

        // Add markers for each telemetry data point
        const startTime = telemetryData[0].timestamp;

        telemetryData.forEach((data) => {
            const latLng = [data.latitude, data.longitude] as [number, number];
            const secondsAfterStart = (
                (new Date(data.timestamp).getTime() - new Date(startTime).getTime()) /
                1000
            ).toFixed(0);
            const markerIcon = L.divIcon({
                className: 'drone-marker-icon',
                html: '<div style="background-color: blue; width: 8px; height: 8px; border-radius: 50%;"></div>',
                iconSize: [8, 8],
                iconAnchor: [4, 4],
                popupAnchor: [0, -4],
            });
            const marker = L.marker(latLng, { icon: markerIcon })
                .addTo(this.map!)
                .bindPopup(`Time: ${secondsAfterStart} seconds`);

            this.droneMarkers.push(marker);
        });
    }
}
