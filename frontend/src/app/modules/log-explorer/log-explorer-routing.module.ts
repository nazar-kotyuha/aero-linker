import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LogExplorerComponent } from './log-explorer/log-explorer.component';

const routes: Routes = [{ path: '', component: LogExplorerComponent }];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class LogExplorerRoutingModule {}
