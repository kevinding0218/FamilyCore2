import { UserService } from './../../services/member/user.service';
import { UserRegistration } from './../../viewModels/member/user.registration';
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

  constructor(private _route: ActivatedRoute,
    private _router: Router,
    private _upService: UserPasswordService,
    private _userService: UserService,
    private toastr: ToastrService
  ) { }

  // Old
  // newUser: RegisterInfo = {
  //   email: '',
  //   password: ''
  // };
  // submit() {
  //   this._upService.create(this.newUser)
  //     .subscribe(result => {
  //       this.toastr.success('Please login with your new account!', 'ACCOUNT CREATED SUCCESS');
  //       this._router.navigate(['/pages/login']);
  //     },
  //     (err) => {
  //       HelperMethod.subscribeErrorHandler(err, this.toastr);
  //     });
  // }

  // New
  newUser: UserRegistration = {
    email: '',
    password: '',
    firstName: '',
    lastName: '',
    location: ''
  };
  submit() {
    this._userService.register(this.newUser.email, this.newUser.password, this.newUser.firstName, this.newUser.lastName, this.newUser.location)
      .subscribe(
      result => {
        if (result) {
          this._router.navigate(['/pages/login']);
        }
      },
      (err) => {
        HelperMethod.subscribeErrorHandler(err, this.toastr);
      });
  }

}
