import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'entree-form-common',
    templateUrl: './entree-form-common.component.html'
})

export class EntreeFormCommonComponent implements OnInit {
    splitBy: string = '';
    splitId: number = 0;
    updatedId: number = 0;


    constructor(
        private _route: ActivatedRoute
    ) {
        _route.params.subscribe(p => {
            this.splitBy = (typeof p['splitBy'] == 'undefined') ? 'update' : p['splitBy'];
            if (this.splitBy == 'update')
            {
                this.updatedId = (typeof p['id'] == 'undefined') ? 0 : +p['id'];
            } else {
                this.splitId = (typeof p['splitId'] == 'undefined') ? 0 : +p['splitId'];
            }
        });
    }

    ngOnInit() {}
}