import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ControlsModule } from '@modules/controls/controls.module';
import { SharedModule } from '@shared/shared.module';

import { LogExplorerComponent } from './log-explorer/log-explorer.component';
import { LogMapComponent } from './log-map/log-map.component';
import { LogExplorerRoutingModule } from './log-explorer-routing.module';

@NgModule({
    declarations: [LogExplorerComponent, LogMapComponent],
    imports: [CommonModule, LogExplorerRoutingModule, SharedModule, ControlsModule],
})
export class LogExplorerModule {}
