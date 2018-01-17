import { SaveEntreeDetail } from './../../../../viewModels/meal/entreeDetail';
import { NgForm } from '@angular/forms/src/directives';
import { Observable } from 'rxjs/Rx';
import { ToastrService } from 'ngx-toastr';
import { StaplefoodService } from './../../../../services/meal/staplefood.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-staplefood-form',
    templateUrl: './staplefood-form.component.html'
})
export class StaplefoodFormComponent implements OnInit {
    FormHeader: string = 'Update staplefood Item';
    isDevelopment: boolean = (JSON.parse(localStorage.getItem('currentUser')) == 'true' ? true : false);
    successfulSave: boolean = false;
    oldName: string;
    oldNote: string;
    staplefood: SaveEntreeDetail = {
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
        private _route: ActivatedRoute,
        private _router: Router,
        private _staplefoodService: StaplefoodService,
        private toastr: ToastrService
    ) {
        _route.params.subscribe(p => {
            this.staplefood.keyValuePairInfo.id = (typeof p['id'] == 'undefined') ? 0 : +p['id'];
        });
    }

    ngOnInit() {
        var sources = [];

        if (this.staplefood.keyValuePairInfo.id)
        {
            sources.push(this._staplefoodService.getStaplefood(this.staplefood.keyValuePairInfo.id));

            Observable.forkJoin(sources).subscribe(data => {
                if (this.staplefood.keyValuePairInfo.id) {
                    this.FormHeader = 'Update staplefood Item';
                    this.setstaplefood(data[0]);
                }
            }, err => {
                if (err.status == 404)
                    this._router.navigate(['/pages/404']);
            });
        } else {
            this.FormHeader = 'Create New staplefood';
        }
    }

    private setstaplefood(vege: any) {
        this.staplefood.keyValuePairInfo.name = vege.keyValuePairInfo.name;
        this.staplefood.addedOn = vege.addedOn;
        this.staplefood.addedById = vege.addedById;
        this.staplefood.updatedOn = vege.updatedOn;
        this.staplefood.lastUpdatedById = vege.lastUpdatedById;
        this.staplefood.note = vege.note;
    }

    submit() {
        if (this.staplefood.keyValuePairInfo.id) {
            this.staplefood.lastUpdatedById = 2;

            this._staplefoodService.update(this.staplefood)
                .subscribe(
                (data) => {
                    this.successfulSave = true;
                    this._router.navigate(['/meal/staplefoodList']);
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
            this.staplefood.addedById = 2;
            this.staplefood.addedOn = new Date();
            this._staplefoodService.create(this.staplefood)
                .subscribe(x => {
                    this.toastr.success('This staplefood has been successfully inserted!', 'INSERT SUCCESS');
                    this._router.navigate(['/meal/staplefoodList']);
                });
        }
    }

    resetFormValue() {
        this.staplefood.keyValuePairInfo.name = this.oldName;
        this.staplefood.note = this.oldNote;
    }

    deletestaplefood() {
        if (confirm("Are you sure?")) {
            // this._staplefoodService.delete(this.staplefood.keyValuePairInfo.id)
            //     .subscribe(x => {
            //         this._router.navigate(['/meal/staplefoodForm/new']);
            //     });
        }
    }

}
