import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-entree-by-stle-list',
    templateUrl: './entree-list-by-style.component.html'
})

export class EntreeListByStyleComponent implements OnInit {
    splitBy: string = '';
    splitById: number = null;
    entreeListFormHeader: string = '';
    newEntreeButtonText: string = '';

    constructor( 
        private _route: ActivatedRoute
    ) 
    { 
        _route.params.subscribe(p => {
            this.splitBy = 'style';
            this.splitById = 12;
            this.entreeListFormHeader = 'List of Entrees By Style';
            this.newEntreeButtonText = 'Create New Entree';
        });
    }

    ngOnInit() {  }

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