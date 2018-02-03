import { EntreeDetailService } from '../../../../../services/meal/entree-detail.service';
import { StaplefoodService } from '../../../../../services/meal/staplefood.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { SaveEntreeDetail } from '../../../../../viewModels/meal/entreeDetail';
import { Observable } from 'rxjs/Rx';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuService } from '../../../../../services/index';

@Component({
    selector: 'entree-detail-form',
    templateUrl: './entree-detail-form.component.html'
})
export class EntreeDetailFormComponent implements OnInit {
    action: string = 'create';
    @Input() entreeDetailFormHeader: string = '';
    @Input() entreeDetailType: string = '';
    @Input() entreeDetail: SaveEntreeDetail = {
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

    @Output('submitEntreeFormClick') submitFormClick = new EventEmitter();
    @Output('resetEntreeDetailFormClick') resetFormClick = new EventEmitter();
    @Output('deleteEntreeDetailFormClick') deleteFormClick = new EventEmitter();

    oldName: string;
    oldNote: string;


    constructor(
        private _route: ActivatedRoute,
        private _router: Router,
        private _entreeDetailService: EntreeDetailService,
        private _staplefoodService: StaplefoodService,
        private toastr: ToastrService,
        private _menuService: MenuService
    ) {
        _route.params.subscribe(p => {
            this.entreeDetail.keyValuePairInfo.id = (typeof p['id'] == 'undefined') ? 0 : +p['id'];
        });
    }

    ngOnInit() {
        var sources = [];

        if (this.entreeDetail.keyValuePairInfo.id) {
            this.action = 'update';
            sources.push(this._entreeDetailService.getEntreeDetail(this.entreeDetail.keyValuePairInfo.id));

            Observable.forkJoin(sources).subscribe(data => {
                if (this.entreeDetail.keyValuePairInfo.id) {
                    this.setEntreeDetail(data[0]);
                }
            }, err => {
                if (err.status == 404)
                    this._router.navigate(['/pages/404']);
            });
        }
    }

    private setEntreeDetail(_data: any) {
        this.oldName = this.entreeDetail.keyValuePairInfo.name = _data.keyValuePairInfo.name;
        this.entreeDetail.addedOn = _data.addedOn;
        this.entreeDetail.addedById = _data.addedById;
        this.entreeDetail.updatedOn = _data.updatedOn;
        this.entreeDetail.lastUpdatedById = _data.lastUpdatedById;
        this.oldNote = this.entreeDetail.note = _data.note;
    }

    submit() {
        if (this.entreeDetail.keyValuePairInfo.id) {
            this.updateEntreeDetail();
        }
        else {
            this.addEntreeDetail();
        }
        this.submitFormClick.emit(this.entreeDetail.keyValuePairInfo.id ? 'updateEntreeDetailForm' : 'createEntreeDetailForm' + ' of ' + this.entreeDetailType);
    }

    resetFormValue() {
        this.entreeDetail.keyValuePairInfo.name = this.oldName;
        this.entreeDetail.note = this.oldNote;
        this.resetFormClick.emit('resetEntreeDetailForm');
    }

    cancelForm() {
        this.returnToList();
    }

    deleteEntreeDetail() {
        this.deleteFormClick.emit('deleteEntreeDetailForm');
    }

    updateEntreeDetail() {
        this.entreeDetail.lastUpdatedById = 2;

        this._entreeDetailService.update(this.entreeDetail)
            .subscribe(
            (data) => {
                this.toastr.success(this.entreeDetailType + ' has been successfully updated!', 'UPDATE SUCCESS');
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

    addEntreeDetail() {
        this.entreeDetail.addedById = 2;
        this.entreeDetail.addedOn = new Date();
        this._entreeDetailService.create(this.entreeDetail)
            .subscribe(
            (data) => {
                this.toastr.success(this.entreeDetailType + ' has been successfully inserted!', 'INSERT SUCCESS');
                this.returnToList();
                this._menuService.sendBadgeUpdateMessage('reloadMenu');
            },
            (err) => {
                if (err.status === 400) {
                    // handle validation error
                    let validationErrorDictionary = JSON.parse(err.text());
                    for (var fieldName in validationErrorDictionary) {
                        if (validationErrorDictionary.hasOwnProperty(fieldName)) {
                            this.toastr.warning('Invalid Insert', validationErrorDictionary[fieldName]);
                        }
                    }
                }
            });
    }

    returnToList() {
        switch (this.entreeDetailType.toLowerCase()) {
            case 'meat':
                this._router.navigate(['/meal/meatList/meat']);
                break;
            case 'vegetable':
                this._router.navigate(['/meal/vegetableList/vegetable']);
                break;
            case 'seafood':
                this._router.navigate(['/meal/seafoodList/seafood']);
                break;
            case 'ingredient':
                this._router.navigate(['/meal/ingredientList/ingredient']);
                break;
            case 'stapleFood':
                this._router.navigate(['/meal/staplefoodList/staplefood']);
                break;
            case 'sauce':
                this._router.navigate(['/meal/sauceList/sauce']);
                break;
        }
    }
}
