import { EntreeDetailMappingResource } from './../../../../../../viewModels/meal/entree';
import { HelperMethod } from './../../../../../../utility/helper/helperMethod';
import { ToastrService } from 'ngx-toastr';
import { element } from 'protractor';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs/Rx';
import { EntreeHelperService } from './../../../../../../services/meal/entree-helper.service';
import { concat } from 'rxjs/observable/concat';

@Component({
    selector: 'entree-detail-common-section',
    templateUrl: './entree-detail-common-section.component.html',
    styles: [
        '.edit_ul_li { display: inline-block;padding-right: 10px; }',
        '.input-sm-space { padding-left: 10px; }',
        '.input-md-space { padding-left: 15px; }',
        '.col-form-label { font-weight: bold; }'
    ]
})


export class EntreeDetailCommonSectionComponent implements OnInit {
    @Input() entreeId: number = 0;
    @Input() entreeDetailType: string;
    @Input() entreeDetailList: any;

    // New Entree Detail
    addNewTrigger: boolean = false;
    newEntreeDetail: EntreeDetailMappingResource = {
        entreeDetailId: 0,
        name: '',
        quantity: 1,
        entreeDetailTypeName: '',
        displayMode: true
    };
    availEntreeDetails: any;
    @Output('addNewEntreeDetail') addNewEntreeDetailEmitter = new EventEmitter();
    @Output('removeEntreeDetail') removeEntreeDetailEmitter = new EventEmitter();


    constructor(
        private _entreeHelperService: EntreeHelperService
        , private toastr: ToastrService
    ) { }

    ngOnInit() {
        var sources = [];
        sources.push(this._entreeHelperService.getEntreeHelperDropdownItems(this.entreeDetailType, this.entreeId));

        Observable.forkJoin(sources).subscribe(data => {
            this.availEntreeDetails = data[0];
        }, err => { });

        this.initNewEntreeDetailObj();
    }

    initNewEntreeDetailObj() {
        this.newEntreeDetail = <EntreeDetailMappingResource>{
            entreeDetailId: 0,
            name: '',
            quantity: 1,
            entreeDetailTypeName: this.entreeDetailType,
            displayMode: true
        };
    }

    addNewEntreeDetail() {
        if (this.availEntreeDetails.length > 0) {
            //set initial dropdown selection and init first add item
            this.newEntreeDetail.entreeDetailId = this.availEntreeDetails[0].id;
            this.newEntreeDetail.name = this.availEntreeDetails[0].name;
        }
        this.addNewTrigger = true;
    }

    onAvailEntreeDetailChange(event) {
        var selectedId = event.target.value;
        var selectedText = HelperMethod.findAnotherAttributeByKey(this.availEntreeDetails, 'id', selectedId, 'name');//this.availEntreeDetails.filter(ed => ed.id == selectedId);

        if (typeof selectedText != 'undefined' && selectedText != 'NotFound') {
            console.log('EntreeDetailCartTemplateComponent onAvailEntreeDetail', selectedText);
            this.newEntreeDetail.name = selectedText;
            this.newEntreeDetail.entreeDetailId = parseInt(selectedId);
        }
    }

    addEntreeDetail() {
        if (this.newEntreeDetail.entreeDetailId == 0) {
            this.toastr.warning('Please select a ' + this.entreeDetailType, 'Warning');
        } else {
            if (this.entreeId == 0 && typeof(this.entreeDetailList) === 'undefined')
                this.entreeDetailList = [];
            var tempEntreeDetaiList = this.entreeDetailList;
            tempEntreeDetaiList.push(this.newEntreeDetail);

            if (HelperMethod.isDuplicateItemInArray(tempEntreeDetaiList, 'entreeDetailId')) {
                tempEntreeDetaiList.pop();
                this.newEntreeDetail.entreeDetailId = 0;
                this.newEntreeDetail.displayMode = false;

                this.toastr.warning(this.newEntreeDetail.name + ' already existed!', 'Duplicate Item');
            } else {
                this.addNewTrigger = false;
                this.newEntreeDetail.entreeDetailTypeName = this.entreeDetailType;
                this.newEntreeDetail.displayMode = true;
                this.addNewEntreeDetailEmitter.emit(this.newEntreeDetail);
                this.initNewEntreeDetailObj();
            }
        }
    }

    cancelEntreeDetail() {
        this.addNewTrigger = false;
    }

    // triggered from child component
    OnSaveEntreeDetailClick(entreeDetail) {
        console.log('EntreeDetailCommonSectionComponent OnSaveEntreeDetailClick received', entreeDetail);

        if (HelperMethod.isDuplicateItemInArray(this.entreeDetailList, 'entreeDetailId')) {
            entreeDetail.displayMode = false;
            this.toastr.warning(entreeDetail.name + ' already existed!', 'Duplicate Item');
        } else {
            entreeDetail.displayMode = true;
        }
    }

    OnRemoveEntreeDetailClick(entreeDetail) {
        console.log('EntreeDetailCommonSectionComponent OnRemoveEntreeDetailClick received', entreeDetail);
        this.removeEntreeDetailEmitter.emit(entreeDetail);
    }
}