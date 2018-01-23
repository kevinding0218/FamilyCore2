import { SaveCurrentOrder } from './../../viewModels/order/saveCurrentOrder';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';


@Injectable()
export class CurrentOrderService {
    private readonly apiEndPoint: string = 'http://localhost:49934/api/currentOrder';
    constructor(private _http: Http) { }

    //   getEntree(id) {
    //     return this._http.get(this.apiEndPoint + '/' + id)
    //       .map(res => res.json());
    //   }

    //   //api/entree/group?splitBy=a&id=b
    //   getEntrees(splitBy, id) {
    //     return this._http.get(this.apiEndPoint + '/group?splitBy=' + splitBy + '&id=' + id)
    //       .map(res => res.json());
    //   }

    getOrderIdByCurrentDate(currentDate: string) {
        return this._http.get(this.apiEndPoint + '/' + currentDate)
            .map(res => res.json());
    }

    createEntree(order: SaveCurrentOrder) {
        return this._http.post(this.apiEndPoint, order)
            .map(res => res.json());
    }

    //   updateEntree(entree: SaveEntree) {
    //     return this._http.put(this.apiEndPoint + '/' + entree.id, entree)
    //       .map(res => res.json());
    //   }

    //   deleteEntree(id) {
    //     return this._http.delete(this.apiEndPoint + '/' + id)
    //       .map(res => res.json());
    //   }

}