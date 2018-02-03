import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-sauce-list',
    templateUrl: './sauce-list.component.html'
})

export class SauceListComponent implements OnInit {
    entreeDetailType: string = '';
    entreeListFormHeader: string = '';
    newEntreeButtonText: string = '';

    constructor( 
        private _route: ActivatedRoute
    ) 
    { 
        _route.params.subscribe(p => {
            let entree_type : string = (typeof p['type'] == 'undefined') ? 'entreeDetail' : p['type'];
            //console.log('In SauceListComponent entree_type is ' + entree_type);

            this.entreeDetailType = entree_type;
            this.entreeListFormHeader = 'List of ' + entree_type.capitalizeFirstLetter();
            this.newEntreeButtonText = 'Create New ' + entree_type.capitalizeFirstLetter();
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