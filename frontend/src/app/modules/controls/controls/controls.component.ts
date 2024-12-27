import { Component } from '@angular/core';
import { BaseComponent } from '@core/base/base.component';

@Component({
    selector: 'app-controls',
    templateUrl: './controls.component.html',
    styleUrls: ['./controls.component.sass'],
})
export class ControlsComponent extends BaseComponent {
    // eslint-disable-next-line @typescript-eslint/no-useless-constructor
    constructor() {
        super();
    }
}
