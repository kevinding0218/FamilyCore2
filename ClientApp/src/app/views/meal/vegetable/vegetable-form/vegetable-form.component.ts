import { NgForm } from '@angular/forms/src/directives';
import { Observable } from 'rxjs/Rx';
import { ActivatedRoute, Router } from '@angular/router';
import { VegetableService } from './../../../../services/meal/vegetable.service';
import { SaveVegetable, KeyValuePairInfo } from './../../../../viewModels/meal/vegetable';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-vegetable-form',
  templateUrl: './vegetable-form.component.html'
})
export class VegetableFormComponent implements OnInit {
  FormHeader: string = 'Update Vegetable Item';
  isDevelopment: boolean = (JSON.parse(localStorage.getItem('currentUser')) == 'true' ? true : false);
  successfulSave: boolean = false;
  oldName: string;
  oldNote: string;
  vegetable: SaveVegetable = {
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
    private _vegetableService: VegetableService,
    private toastr: ToastrService
  ) {
    _route.params.subscribe(p => {
      this.vegetable.keyValuePairInfo.id = (typeof p['id'] == 'undefined') ? 0 : +p['id'];
    });
  }

  ngOnInit() {
    var sources = [];

    if (this.vegetable.keyValuePairInfo.id) {
      sources.push(this._vegetableService.getVegetable(this.vegetable.keyValuePairInfo.id));

      Observable.forkJoin(sources).subscribe(data => {
        if (this.vegetable.keyValuePairInfo.id) {
          this.setVegetable(data[0]);
        }
      }, err => {
        if (err.status == 404)
          this._router.navigate(['/pages/404']);
      });
    } else {
      this.FormHeader = 'Create New Vegetable';
    }
  }

  private setVegetable(vege: any) {
    this.oldName = this.vegetable.keyValuePairInfo.name = vege.keyValuePairInfo.name;
    this.vegetable.addedOn = vege.addedOn;
    this.vegetable.addedByUserId = vege.addedByUserId;
    this.vegetable.updatedOn = vege.updatedOn;
    this.vegetable.lastUpdatedByUserId = vege.lastUpdatedByUserId;
    this.oldNote = this.vegetable.note = vege.note;
  }

  submit() {
    if (this.vegetable.keyValuePairInfo.id) {
      this.vegetable.lastUpdatedByUserId = 2;

      this._vegetableService.update(this.vegetable)
        .subscribe(
        (data) => {
          this.successfulSave = true;
          this._router.navigate(['/meal/vegetableList']);
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
      this.vegetable.addedByUserId = 2;
      this.vegetable.addedOn = new Date();
      this._vegetableService.create(this.vegetable)
        .subscribe(x => {
          this.toastr.success('This vegetable has been successfully inserted!', 'INSERT SUCCESS');
          this._router.navigate(['/meal/vegetableList']);
        });
    }
  }

  resetFormValue() {
    this.vegetable.keyValuePairInfo.name = this.oldName;
    this.vegetable.note = this.oldNote;
  }

  deleteVegetable() {
    if (confirm("Are you sure?")) {
      // this._vegetableService.delete(this.vegetable.keyValuePairInfo.id)
      //     .subscribe(x => {
      //         this._router.navigate(['/meal/vegetableForm/new']);
      //     });
    }
  }

}
