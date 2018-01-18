import { EntreeHelperService } from './../../../../../services/meal/entree-helper.service';
import { SaveEntree } from './../../../../../viewModels/meal/entree';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/Rx';
import { ToastrService } from 'ngx-toastr';
import { KeyValuePairInfo } from '../../../../../viewModels/meal/entreeDetail';

@Component({
    selector: 'entree-form-common',
    templateUrl: './entree-form-common.component.html',
    styles:[
        '.input-sm-space { padding-left: 10px; }',
        '.input-md-space { padding-left: 15px; }'
    ]
})

export class EntreeFormCommonComponent implements OnInit {
    action: string = 'create';
    splitBy: string = '';
    splitId: number = 0;
    updatedId: number = 0;

    // dropdown items
    entreeStyles: any;
    entreeCatagories: any;
    stapleFoods: any;

    // vegetable
    availVegetables: any;
    showVegetableDropdown: boolean = false;
    

    @Input() entree: SaveEntree = {
        id: 0,
        name: '',
        stapleFoodId: 8,
        entreeCatagoryId: 9,
        entreeStyleId: 12,
        currentRank: 3,
        addedOn: null,
        addedById: 0,
        updatedOn: null,
        lastUpdatedById: 0,
        note: '',
        entreeDetailIds: [],
        vegetables: '青菜, 大白菜',
        meats: '牛腱',
        seafoods: '',
        sauces: '',
        ingredients: ''
    };

    @Input() entreeFormCommonHeader: string = '';

    @Output('submitFormClick') submitFormClick = new EventEmitter();
    @Output('resetFormClick') resetFormClick = new EventEmitter();
    @Output('deleteFormClick') deleteFormClick = new EventEmitter();


    constructor(
        private _route: ActivatedRoute,
        private _router: Router,
        private _entreeHelperService: EntreeHelperService,
        private toastr: ToastrService
    ) {
        _route.params.subscribe(p => {
            this.splitBy = (typeof p['splitBy'] == 'undefined') ? 'update' : p['splitBy'];
            if (this.splitBy == 'update') {
                this.updatedId = (typeof p['id'] == 'undefined') ? 0 : +p['id'];
            } else {
                this.splitId = (typeof p['splitId'] == 'undefined') ? 0 : +p['splitId'];
            }
        });
    }

    ngOnInit() {
        var sources = [];
        sources.push(this._entreeHelperService.getEntreeHelperDropdownItems('style', 0));
        sources.push(this._entreeHelperService.getEntreeHelperDropdownItems('catagory', 0));
        sources.push(this._entreeHelperService.getEntreeHelperDropdownItems('staplefood', 0));

        Observable.forkJoin(sources).subscribe(data => {
            this.entreeStyles = data[0];
            this.entreeCatagories = data[1];
            this.stapleFoods = data[2];
        }, err => {
            if (err.status == 404)
                this._router.navigate(['/pages/404']);
        });

        if (this.updatedId != 0) {
            this.action = 'update';
        }
    }

    // Rank Control
    getColor():string {
        return (this.entree.currentRank > 4) ? 'positive': ((this.entree.currentRank >= 3) ? 'ok':'negative');
    };

    onClick(event) {
        console.log('onClick ', event);
        this.entree.currentRank = event.rating;
    }

    // Button Event
    submit() {
        this.submitFormClick.emit('submitEntreeForm id: ' + this.updatedId);
    }

    resetFormValue() {
        this.resetFormClick.emit('resetEntreeForm');
    }

    cancelForm() {
        this.returnToList();
    }

    deleteEntree() {
        this.deleteFormClick.emit('deleteEntreeForm');
    }

    returnToList() {
        switch (this.splitBy.toLowerCase()) {
            case 'style':
                this._router.navigate(['/meal/entreeListByStyle']);
                break;
            case 'catagory':
                this._router.navigate(['/meal/entreeListByStyle']);
                break;
        }
    }

    // Entree Detail
    readyToAddVegetable() {
        this.showVegetableDropdown = true;
    }

    addSelectedVegetable() {
        this.showVegetableDropdown = false;
    }

    cancelAddVegetables() {
        this.showVegetableDropdown = false;
    }
}