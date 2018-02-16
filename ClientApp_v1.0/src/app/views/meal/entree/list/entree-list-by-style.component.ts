import { Observable } from 'rxjs/Rx';
import { EntreeHelperService } from './../../../../services/meal/entree-helper.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { routerTransition } from '../../../../router.animations';

@Component({
    selector: 'app-entree-by-stle-list',
    templateUrl: './entree-list-by-style.component.html',
    animations: [routerTransition()]
})

export class EntreeListByStyleComponent implements OnInit {
    splitBy: string = '';
    splitById: number = null;
    entreeStyles: any;
    entreeCatagories: any;

    entreeListFormHeader: string = '';
    newEntreeButtonText: string = '';

    constructor( 
        private _route: ActivatedRoute,
        private _router: Router,
        private _entreeHelperService: EntreeHelperService
    ) 
    { 
        _route.params.subscribe(p => {
            this.splitBy = (typeof p['splitBy'] == 'undefined') ? '' : p['splitBy'];
            this.splitById = (typeof p['splitId'] == 'undefined') ? 0 : +p['splitId'];
        });
    }

    ngOnInit() { 
        var sources = [];
        sources.push(this._entreeHelperService.getEntreeHelperDropdownItems('style', 0));
        sources.push(this._entreeHelperService.getEntreeHelperDropdownItems('catagory', 0));

        Observable.forkJoin(sources).subscribe(data => {
            this.entreeStyles = data[0];
            this.entreeCatagories = data[1];

            let splitByName: string = '';
            if (this.splitBy == 'style')
            {
                var currentStyle = this.entreeStyles.filter(es => es.id === this.splitById);
                splitByName = currentStyle[0].name;
            } else if (this.splitBy == 'catagory')
            {
                var currentCatagory = this.entreeCatagories.filter(ec => ec.id === this.splitById);
                splitByName = currentCatagory[0].name;
            }
    
            this.entreeListFormHeader = 'List of Entrees Of ' + splitByName;
            this.newEntreeButtonText = 'Create New ' + splitByName;

        }, err => {
            if (err.status == 404)
                this._router.navigate(['/pages/404']);
        });
     }

    OnEntreeDetailCreateNewClick(eventArgs){
        console.log('OnEntreeDetailCreateNewClick');
        console.log(eventArgs);
    }

    OnEntreeDetailEditRowClick(eventArgs){
        console.log('OnEntreeDetailEditRowClick');
        console.log(eventArgs);
    }

    OnEntreeDetailToggleExpandRow(eventArgs){
        console.log('OnEntreeDetailToggleExpandRow');
        console.log(eventArgs);
    }
}