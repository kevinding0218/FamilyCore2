import { SaveInitialOrder, OrderProcessInfo } from './../../viewModels/order/saveOrder';
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

    getOrderByOrderId(orderId, includeMapping, includeEntree) {
        return this._http.get(this.apiEndPoint + '/byId?orderId=' + orderId 
                                            + '&includeMapping=' + includeMapping 
                                            + '&includeEntree=' + includeEntree)
            .map(res => res.json());
    }

    getOrderIdByCurrentDate(currentDate: string) {
        return this._http.get(this.apiEndPoint + '/byCurrentDate?currentDateStr=' + currentDate)
            .map(res => res.json());
    }

    getCurrentWeekOrderPrepare() {
        return this._http.get(this.apiEndPoint + '/currentWeekOrderPrepare')
            .map(res => res.json());
    }

    getCurrentWeekOrderEntreeDetails() {
        return this._http.get(this.apiEndPoint + '/currentWeekOrderEntreeDetails')
            .map(res => res.json());
    }

    createOrder(order: SaveInitialOrder) {
        return this._http.post(this.apiEndPoint, order)
            .map(res => res.json());
    }

    updateInitialOrder(order: SaveInitialOrder) {
        return this._http.put(this.apiEndPoint + '/updateInitialOrder/' + order.id, order)
            .map(res => res.json());
    }

    updateProcessingOrder(order: OrderProcessInfo) {
        return this._http.put(this.apiEndPoint + '/updateProcessingOrder/' + order.id, order)
            .map(res => res.json());
    }

    //   deleteEntree(id) {
    //     return this._http.delete(this.apiEndPoint + '/' + id)
    //       .map(res => res.json());
    //   }

}