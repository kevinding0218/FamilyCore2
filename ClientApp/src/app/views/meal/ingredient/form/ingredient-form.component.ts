import { Component, OnInit } from '@angular/core';
import { SaveEntreeDetail } from '../../../../viewModels/meal/entreeDetail';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-ingredient-form',
    templateUrl: './ingredient-form.component.html'
})
export class IngredientFormComponent implements OnInit {
    entreeDetailFormHeader: string = '';
    entreeDetailType: string = '';
    entreeDetail: SaveEntreeDetail = {
        keyValuePairInfo: {
            id: null,
            name: ''
        },
        addedOn: null,
        addedById: null,
        updatedOn: null,
        lastUpdatedById: null,
        detailType: '',
        note: ''
    };

    constructor(
        private _route: ActivatedRoute
    ) {
        _route.params.subscribe(p => {
            let entree_type: string = (typeof p['type'] == 'undefined') ? 'entreeDetail' : p['type'];
            let sauce_id = (typeof p['id'] == 'undefined') ? 0 : +p['id'];
            console.log('In IngredientFormComponent entree_type is ' + entree_type + '\nsauce_id is ' + sauce_id);
            this.entreeDetail.keyValuePairInfo.id = sauce_id;
            this.entreeDetailType = entree_type.capitalizeFirstLetter();
            if (sauce_id != 0)
                this.entreeDetailFormHeader = 'Update ' + entree_type.capitalizeFirstLetter() + ' Form';
            else
                this.entreeDetailFormHeader = 'Create ' + entree_type.capitalizeFirstLetter() + ' Form';
        });
    }

    ngOnInit() { }

    OnEntreeDetailSubmitClick(eventArgs) {
        console.log('OnEntreeDetailSubmitClick');
        console.log(eventArgs);
    }

    OnEntreeDetailResetClick(eventArgs) {
        console.log('OnEntreeDetailResetClick');
        console.log(eventArgs);
    }

    OnEntreeDetailDeleteClick(eventArgs) {
        console.log('OnEntreeDetailDeleteClick');
        console.log(eventArgs);
    }
}