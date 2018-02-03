import { Observable } from 'rxjs/Rx';
import { Subject } from 'rxjs/Subject';
import { SaveInitialOrder, OrderProcessInfo, EntreeOrderMappingSchedule } from './../../viewModels/order/saveOrder';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';


@Injectable()
export class MenuService {
    private readonly apiPort: string = localStorage.getItem('WebApiPath').toString();
    private readonly apiEndPoint: string = this.apiPort + '/applicationMenu';
    constructor(private _http: Http) { }

    getNavigations(userId) {
        return this._http.get(this.apiEndPoint)
            .map(res => res.json());
    }

    // Badge state trigger subject
    private navBadgeStateSource = new Subject<any>();
    setNavBarState(state: any) {
        this.navBadgeStateSource.next(state);
    }

    sendBadgeUpdateMessage(message: string) {
        this.navBadgeStateSource.next({ info: message });
    }
 
    clearBadgeUpdateMessage() {
        this.navBadgeStateSource.next();
    }
 
    getBadgeUpdateMessage(): Observable<any> {
        return this.navBadgeStateSource.asObservable();
    }
}