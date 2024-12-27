import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

import { ControlsComponent } from './controls/controls.component';
import { MapComponent } from './map/map.component';
import { ControlsRoutingModule } from './controls-routing.module';

@NgModule({
    declarations: [ControlsComponent, MapComponent],
    imports: [CommonModule, ControlsRoutingModule, SharedModule],
})
export class ControlsModule {}
