import { EntreeService } from './../../../../../services/meal/entree.service';
import { EntreeHelperService } from './../../../../../services/meal/entree-helper.service';
import { SaveEntree, EntreeDetailMappingResource } from './../../../../../viewModels/meal/entree';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/Rx';
import { ToastrService } from 'ngx-toastr';
import { KeyValuePairInfo } from '../../../../../viewModels/meal/entreeDetail';

@Component({
    selector: 'entree-form-common',
    templateUrl: './entree-form-common.component.html',
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
    entreeDetailTypes: any;

    vegetables: any;
    meats: any;



    @Input() entree: SaveEntree = {
        id: 0,
        name: '',
        stapleFoodId: 8,
        entreeCatagoryId: 9,
        entreeStyleId: 12,
        currentRank: 3,
        addedOn: null,
        addedById: 0,
        lastUpdatedByOn: null,
        lastUpdatedById: 0,
        note: '',
        entreeDetails: []
    };

    @Input() entreeFormCommonHeader: string = '';

    @Output('submitFormClick') submitFormClick = new EventEmitter();
    @Output('resetFormClick') resetFormClick = new EventEmitter();
    @Output('deleteFormClick') deleteFormClick = new EventEmitter();


    constructor(
        private _route: ActivatedRoute,
        private _router: Router,
        private _entreeService: EntreeService,
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
        sources.push(this._entreeHelperService.getEntreeHelperDropdownItems('entreeDetailType', 0));
        if (this.updatedId != 0)
            sources.push(this._entreeService.getEntree(this.updatedId));
        else {
            this.entreeFormCommonHeader = 'Create New Entree';
            this.entree.entreeDetails = [];
        }

        Observable.forkJoin(sources).subscribe(data => {
            this.entreeStyles = data[0];
            this.entreeCatagories = data[1];
            this.stapleFoods = data[2];
            this.entreeDetailTypes = data[3];
            if (this.updatedId != 0)
                this.setEntree(data[4]);
        }, err => {
            if (err.status == 404)
                this._router.navigate(['/pages/404']);
        });

        if (this.updatedId != 0) {
            this.action = 'update';
        }
    }

    private setEntree(_data: any) {
        this.entree.id = _data.id;
        this.entree.name = _data.name;
        this.entree.stapleFoodId = _data.stapleFoodId;
        this.entree.note = _data.note;
        this.entree.entreeCatagoryId = _data.entreeCatagoryId;
        this.entree.entreeStyleId = _data.entreeStyleId;
        this.entree.currentRank = _data.currentRank;
        this.entree.addedById = _data.addedById;
        this.entree.addedOn = _data.addedOn;
        this.entree.lastUpdatedById = _data.lastUpdatedById;
        this.entree.lastUpdatedByOn = _data.lastUpdatedByOn;
        this.entree.entreeDetails = _data.entreeDetails;

        this.entreeFormCommonHeader = 'Update Entree - ' + this.entree.name;
    }

    filterEntreeDetailList(detailType) {
        if (this.entree.entreeDetails.length > 0) {
            return this.entree.entreeDetails.filter(ed => ed.entreeDetailTypeName === detailType);
        }
    }

    // Rank Control
    getColor(): string {
        return (this.entree.currentRank > 4) ? 'positive' : ((this.entree.currentRank >= 3) ? 'ok' : 'negative');
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

    // triggered from child component
    addNewEntreeDetailTrigger(newEntreeDetail) {
        //console.log('EntreeFormCommonComponent addNewEntreeDetailTrigger received', newEntreeDetail);
        this.entree.entreeDetails.push(newEntreeDetail);
    }

    removeEntreeDetailTrigger(entreeDetail) {
        console.log('EntreeFormCommonComponent removeEntreeDetailTrigger received', entreeDetail);
        let removedIndex = this.entree.entreeDetails.indexOf(entreeDetail);
        if (removedIndex > -1) {
            this.entree.entreeDetails.splice(removedIndex, 1);
        }

        this.toastr.success(entreeDetail.name + ' has been removed!', 'Removed Notification');
    }
}