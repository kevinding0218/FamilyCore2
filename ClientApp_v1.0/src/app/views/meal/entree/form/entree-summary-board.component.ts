import { Observable } from 'rxjs/Rx';
import { EntreeHelperService } from './../../../../services/meal/entree-helper.service';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-entree-summary-board',
    templateUrl: './entree-summary-board.component.html'
})

export class EntreeSummaryBoardComponent implements OnInit {
    entreeCountByCatagory: any;
    entreeCountByStyle: any;

    constructor(
        private _entreeHelperService: EntreeHelperService
    ) { }

    ngOnInit() { 
        var sources = [];
        sources.push(this._entreeHelperService.getEntreeCountBySplit('catagory'));
        sources.push(this._entreeHelperService.getEntreeCountBySplit('style'));

        Observable.forkJoin(sources).subscribe(data => {
            this.entreeCountByCatagory = data[0];
            this.entreeCountByStyle = data[1];
        }, err => {
            
        });
     }
}