import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ControlsComponent } from '@modules/controls/controls/controls.component';

const routes: Routes = [{ path: '', component: ControlsComponent }];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ControlsRoutingModule {}
