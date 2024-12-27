import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { UserDto } from 'src/app/models/user/user-dto';

@Injectable({
    providedIn: 'root',
})
export class EventService {
    private onUserChanged = new BehaviorSubject<UserDto | undefined>(undefined);

    get userChangedEvent$() {
        return this.onUserChanged.asObservable();
    }

    public userChanged(user: UserDto | undefined) {
        this.onUserChanged.next(user);
    }
}
