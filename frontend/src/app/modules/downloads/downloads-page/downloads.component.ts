import { Component } from '@angular/core';
import { FilesDownloaderService } from '@core/services/files-downloader.service';

@Component({
    selector: 'app-downloads',
    templateUrl: './downloads.component.html',
    styleUrls: ['./downloads.component.sass'],
})
export class DownloadsComponent {
    constructor(private filesDownloader: FilesDownloaderService) {}

    public downloadApp(): void {
        this.filesDownloader.downloadAeroLinkerInstaller();
    }
}
