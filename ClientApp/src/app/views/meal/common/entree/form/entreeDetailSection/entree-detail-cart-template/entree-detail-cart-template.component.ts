import { HelperMethod } from '../../../../../../../utility/helper/helperMethod';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs/Rx';
import { EntreeHelperService } from '../../../../../../../services/meal/entree-helper.service';

@Component({
    selector: 'entree-detail-cart-template',
    templateUrl: './entree-detail-cart-template.component.html',
    styleUrls: ['./entree-detail-cart.css']
})


export class EntreeDetailCartTemplateComponent implements OnInit {
    @Input() entreeId: number = 0;
    @Input() entreeDetailType: string = '';
    @Input() entreeDetail: any;

    @Output('saveEntreeDetail') saveEntreeDetailEmitter = new EventEmitter();
    @Output('removeEntreeDetail') removeEntreeDetailEmitter = new EventEmitter();
    // entreeDetail
    availEntreeDetails: any;
    showEntreeDetailDropdown: boolean = false;

    constructor(
        private _entreeHelperService: EntreeHelperService
    ) { }

    ngOnInit() {
        var sources = [];
        sources.push(this._entreeHelperService.getEntreeHelperDropdownItems(this.entreeDetailType, this.entreeId));

        if (this.availEntreeDetails == null || this.availEntreeDetails.length == 0) {
            Observable.forkJoin(sources).subscribe(data => {
                this.availEntreeDetails = data[0];
            }, err => { });
        }
    }

    // Entree Detail
    readyToAddEntreeDetail() {
        this.showEntreeDetailDropdown = true;
    }

    addEntreeDetail() {
        this.showEntreeDetailDropdown = false;
    }

    onAvailEntreeDetailChange(event) {
        var selectedId = event.target.value;
        var selectedText = HelperMethod.findAnotherAttributeByKey(this.availEntreeDetails, 'id', selectedId, 'name');//this.availEntreeDetails.filter(ed => ed.id == selectedId);

        if (typeof selectedText != 'undefined' && selectedText != 'NotFound') {
            console.log('EntreeDetailCartTemplateComponent onAvailEntreeDetail', selectedText);
            this.entreeDetail.name = selectedText;
            this.entreeDetail.entreeDetailId = parseInt(selectedId);
        }
    }



    editEntreeDetail(entreeDetail) {
        entreeDetail.displayMode = false;
    }

    removeEntreeDetail(entreeDetail) {
        console.log(entreeDetail);
        this.removeEntreeDetailEmitter.emit(entreeDetail);
    }

    saveEntreeDetail(entreeDetail) {
        console.log(entreeDetail);
        this.saveEntreeDetailEmitter.emit(entreeDetail);
        //entreeDetail.displayMode = true;
    }

    cancelEntreeDetail(entreeDetail) {
        entreeDetail.displayMode = true;
    }
}