import { NgForm } from '@angular/forms/src/directives';
import { Observable } from 'rxjs/Rx';
import { ToastrService } from 'ngx-toastr';
import { MeatService } from './../../../../services/meal/meat.service';
import { ActivatedRoute, Router } from '@angular/router';
import { SaveMeat } from './../../../../viewModels/meal/meat';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-meat-form',
    templateUrl: './meat-form.component.html'
})
export class MeatFormComponent implements OnInit {
    FormHeader: string = 'Update Meat Item';
    isDevelopment: boolean = (JSON.parse(localStorage.getItem('currentUser')) == 'true' ? true : false);
    successfulSave: boolean = false;
    oldName: string;
    oldNote: string;
    meat: SaveMeat = {
        keyValuePairInfo: {
            id: null,
            name: ''
        },
        addedOn: null,
        addedByUserId: null,
        updatedOn: null,
        lastUpdatedByUserId: null,
        note: ''
    };
    constructor(
        private _route: ActivatedRoute,
        private _router: Router,
        private _meatService: MeatService,
        private toastr: ToastrService
    ) {
        _route.params.subscribe(p => {
            this.meat.keyValuePairInfo.id = (typeof p['id'] == 'undefined') ? 0 : +p['id'];
        });
    }

    ngOnInit() {
        var sources = [];

        if (this.meat.keyValuePairInfo.id)
        {
            sources.push(this._meatService.getMeat(this.meat.keyValuePairInfo.id));

            Observable.forkJoin(sources).subscribe(data => {
                if (this.meat.keyValuePairInfo.id) {
                    this.FormHeader = 'Update Meat Item';
                    this.setmeat(data[0]);
                }
            }, err => {
                if (err.status == 404)
                    this._router.navigate(['/pages/404']);
            });
        } else {
            this.FormHeader = 'Create New Meat';
        }
    }

    private setmeat(vege: any) {
        this.meat.keyValuePairInfo.name = vege.keyValuePairInfo.name;
        this.meat.addedOn = vege.addedOn;
        this.meat.addedByUserId = vege.addedByUserId;
        this.meat.updatedOn = vege.updatedOn;
        this.meat.lastUpdatedByUserId = vege.lastUpdatedByUserId;
        this.meat.note = vege.note;
    }

    submit() {
        if (this.meat.keyValuePairInfo.id) {
            this.meat.lastUpdatedByUserId = 2;

            this._meatService.update(this.meat)
                .subscribe(
                (data) => {
                    this.successfulSave = true;
                    this._router.navigate(['/meal/meatList']);
                },
                (err) => {
                    this.successfulSave = false;
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
        } else {
            this.meat.addedByUserId = 2;
            this.meat.addedOn = new Date();
            this._meatService.create(this.meat)
                .subscribe(x => {
                    this.toastr.success('This meat has been successfully inserted!', 'INSERT SUCCESS');
                    this._router.navigate(['/meal/meatList']);
                });
        }
    }

    resetFormValue() {
        this.meat.keyValuePairInfo.name = this.oldName;
        this.meat.note = this.oldNote;
    }

    deleteMeat() {
        if (confirm("Are you sure?")) {
            // this._meatService.delete(this.meat.keyValuePairInfo.id)
            //     .subscribe(x => {
            //         this._router.navigate(['/meal/meatForm/new']);
            //     });
        }
    }

}
