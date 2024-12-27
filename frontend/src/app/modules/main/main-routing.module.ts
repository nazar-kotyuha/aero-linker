import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from '@shared/components/not-found/not-found.component';

import { MainComponent } from './main-page/main-page.component';

const routes: Routes = [
    {
        path: '',
        component: MainComponent,
        children: [
            {
                path: 'controls',
                loadChildren: () => import('../controls/controls.module').then((m) => m.ControlsModule),
            },
            {
                path: 'log-explorer',
                loadChildren: () => import('../log-explorer/log-explorer.module').then((m) => m.LogExplorerModule),
            },
            {
                path: 'settings',
                loadChildren: () => import('../settings/settings.module').then((m) => m.SettingsModule),
            },
            {
                path: '**',
                component: NotFoundComponent,
                pathMatch: 'full',
            },
        ],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MainRoutingModule {}
