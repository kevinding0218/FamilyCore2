import { HelperMethod } from './../../utility/helper/helperMethod';
import { Router, ActivatedRoute } from '@angular/router';
import { UserPasswordService } from './../../services/user/userpassword/userpassword.service';
import { ToastrService } from 'ngx-toastr';
import { RegisterInfo } from './../../viewModels/user/user';
import { Component } from '@angular/core';

@Component({
  templateUrl: 'register.component.html'
})
export class RegisterComponent {
  newUser: RegisterInfo = {
    email: '',
    password: ''
  };
  constructor(private _route: ActivatedRoute,
    private _router: Router,
    private _upService: UserPasswordService,
    private toastr: ToastrService
  ) { }

  submit() {
    this._upService.create(this.newUser)
      .subscribe(result => {
        this.toastr.success('Please login with your new account!', 'ACCOUNT CREATED SUCCESS');
        this._router.navigate(['/pages/login']);
      },
      (err) => {
        HelperMethod.subscribeErrorHandler(err, this.toastr);
      });
  }
}
