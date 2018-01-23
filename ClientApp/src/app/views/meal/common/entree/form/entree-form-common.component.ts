import { element } from 'protractor';
import { KeyValuePairInfo } from './../../../../../viewModels/meal/entreeDetail';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { EntreeService } from './../../../../../services/meal/entree.service';
import { EntreeHelperService } from './../../../../../services/meal/entree-helper.service';
import { SaveEntree, EntreeDetailMappingResource, SimilarEntreeInputObj } from './../../../../../viewModels/meal/entree';
import { Component, OnInit, Input, Output, EventEmitter, TemplateRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/Rx';
import { ToastrService } from 'ngx-toastr';

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

    // Modal
    @ViewChild('infoModal') infoModal: any;
    modalRef: BsModalRef;
    message: string;
    similarEntreeList: KeyValuePairInfo[] = [];

    @Input() entree: SaveEntree = {
        id: 0,
        name: '',
        stapleFoodId: 0,
        entreeCatagoryId: 0,
        entreeStyleId: 0,
        currentRank: 0,
        addedOn: new Date(),
        addedById: 0,
        lastUpdatedByOn: null,
        lastUpdatedById: 0,
        note: '',
        entreeDetails: []
    };

    @Input() entreeFormCommonHeader: string = '';

    @Output('submitEntreeFormClick') submitFormClick = new EventEmitter();
    @Output('resetEntreeFormClick') resetFormClick = new EventEmitter();
    @Output('deleteEntreeFormClick') deleteFormClick = new EventEmitter();


    constructor(
        private _route: ActivatedRoute,
        private _router: Router,
        private _entreeService: EntreeService,
        private _entreeHelperService: EntreeHelperService,
        private toastr: ToastrService,
        private modalService: BsModalService
    ) {
        _route.params.subscribe(p => {
            this.splitBy = (typeof p['splitBy'] == 'undefined') ? '' : p['splitBy'];
            this.splitId = (typeof p['splitId'] == 'undefined') ? 0 : +p['splitId'];
            this.updatedId = (typeof p['id'] == 'undefined') ? 0 : +p['id'];
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
            if (this.splitBy == 'style')
                this.entree.entreeStyleId = this.splitId;
            else if (this.splitBy == 'catagory')
            this.entree.entreeCatagoryId = this.splitId;
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
        this.entree.stapleFoodId = _data.stapleFoodId == null ? 0 : _data.stapleFoodId;
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
        if (this.entree.entreeDetails != null && this.entree.entreeDetails.length > 0) {
            return this.entree.entreeDetails.filter(ed => ed.entreeDetailTypeName === detailType);
        } else {
            return Array<EntreeDetailMappingResource>();
        }
    }

    // Rank Control
    getStarRatingColor(): string {
        return (this.entree.currentRank > 4) ? 'positive' : ((this.entree.currentRank >= 3) ? 'ok' : 'negative');
    };

    onStarRatingClick(event) {
        console.log('onClick ', event);
        this.entree.currentRank = event.rating;
    }

    // Button Event
    submit() {
        this.SavingEntree();
    }

    generateEntreeDetailIdList(entreeDetails) {
        let entreeDetailIdList: number[] = [];
        entreeDetails.forEach(function (element) { entreeDetailIdList.push(element.entreeDetailId) });
        //console.log('entreeDetailIdList', entreeDetailIdList.toString());

        return entreeDetailIdList.join();
    }

    SavingEntree() {
        if (this.entree.entreeDetails.length == 0) {
            this.toastr.warning('Please add at least one material', 'Invalid Operation');
        } else {
            let entreeInputObj: SimilarEntreeInputObj = {
                stapleFoodId: this.entree.stapleFoodId,
                entreeName: this.entree.name,
                entreeDetailIdList: this.generateEntreeDetailIdList(this.entree.entreeDetails)
            };
            this._entreeHelperService.getSimilarEntreeList(entreeInputObj)
                .subscribe(
                (data) => {
                    //console.log(data);
                    if (data != null && data instanceof Array && data.length > 0) {
                        this.similarEntreeList = data;
                        this.infoModal.show();
                    } else {
                        console.log('No similar entree found');
                        this.continueSavingEntree();
                    }
                },
                (err) => {
                    if (err.status === 400) {
                        // handle validation error
                        let validationErrorDictionary = JSON.parse(err.text());
                        for (var fieldName in validationErrorDictionary) {
                            if (validationErrorDictionary.hasOwnProperty(fieldName)) {
                                this.toastr.warning(validationErrorDictionary[fieldName], 'Invalid Insert');
                            }
                        }
                    }
                });
        }
    }

    continueSavingEntree() {
        if (this.entree.id != 0) {
            this.updateEntree();
        }
        else {
            this.addEntree();
        }
        this.submitFormClick.emit(this.entree);
    }

    addEntree() {
        this.entree.addedById = 2;
        this.entree.addedOn = new Date();
        this.entree.stapleFoodId = this.entree.stapleFoodId == 0 ? null: this.entree.stapleFoodId;
        this._entreeService.createEntree(this.entree)
            .subscribe(
            (data) => {
                this.toastr.success(this.entree.name + ' has been successfully inserted!', 'INSERT SUCCESS');
                this.returnToList();
            },
            (err) => {
                if (err.status === 400) {
                    // handle validation error
                    let validationErrorDictionary = JSON.parse(err.text());
                    for (var fieldName in validationErrorDictionary) {
                        if (validationErrorDictionary.hasOwnProperty(fieldName)) {
                            this.toastr.warning(validationErrorDictionary[fieldName], 'Invalid Insert');
                        }
                    }
                }
            });
    }

    updateEntree() {
        this.entree.lastUpdatedById = 2;
        this.entree.stapleFoodId = this.entree.stapleFoodId == 0 ? null: this.entree.stapleFoodId;

        this._entreeService.updateEntree(this.entree)
            .subscribe(
            (data) => {
                this.toastr.success(this.entree.name + ' has been successfully updated!', 'UPDATE SUCCESS');
                this.returnToList();
            },
            (err) => {
                if (err.status === 400) {
                    // handle validation error
                    let validationErrorDictionary = JSON.parse(err.text());
                    for (var fieldName in validationErrorDictionary) {
                        if (validationErrorDictionary.hasOwnProperty(fieldName)) {
                            this.toastr.warning('Invalid Update', validationErrorDictionary[fieldName]);
                        }
                    }
                }
            });
    }

    resetFormValue() {
        this.resetFormClick.emit('resetEntreeForm');
    }

    cancelForm() {
        this.returnToList();
    }

    deleteEntree() {
        if (confirm("Are you sure?")) {
            this._entreeService.deleteEntree(this.entree.id)
                .subscribe(x => {
                    //this._router.navigate(['/meal/vegetableForm/new']);
                    this.toastr.success(this.entree.name + ' has been successfully deleted!', 'DELETE SUCCESS');
                });
        }
        this.deleteFormClick.emit('deleteEntreeForm');
    }

    returnToList() {
        switch (this.splitBy.toLowerCase()) {
            case 'style':
                this._router.navigate(['/meal/entreeListSplitBy/style/' + this.splitId]);
                break;
            case 'catagory':
                this._router.navigate(['/meal/entreeListSplitBy/catagory/' + this.splitId]);
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

    // Modal 
    confirm(): void {
        this.infoModal.hide();
        this.continueSavingEntree();
    }

    decline(): void {
        this.infoModal.hide();
    }
}