import { Component, Input, OnInit } from '@angular/core';

import { ProjectResponseDto } from '../../../models/projects/project-response-dto';

@Component({
    selector: 'app-project-card',
    templateUrl: './project-card.component.html',
    styleUrls: ['./project-card.component.sass'],
})
export class ProjectCardComponent implements OnInit {
    @Input() public project: ProjectResponseDto;

    public engineLogoImage: string = '';

    private postgresSqlLogo: string = '/assets/project-icon.png';

    ngOnInit(): void {
        this.initializeProjectCard();
    }

    private initializeProjectCard(): void {
        this.engineLogoImage = this.postgresSqlLogo;
    }
}
