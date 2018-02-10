import { UserService } from './../../services/member/user.service';
import { Credentials } from './../../viewModels/member/credentials';
import { RegisterInfo } from './../../viewModels/user/user';
import { Router, ActivatedRoute } from '@angular/router';
import { UserPasswordService } from './../../services/user/userpassword/userpassword.service';
import { ToastrService } from 'ngx-toastr';
import { Component } from '@angular/core';
import { HelperMethod } from './../../utility/helper/helperMethod';

@Component({
  templateUrl: 'login.component.html'
})
export class LoginComponent {
  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _upService: UserPasswordService,
    private _userService: UserService,
    private toastr: ToastrService) { 
      localStorage.removeItem('auth_token');
    }

  // Old Login
  // loginUser: RegisterInfo = {
  //   email: '',
  //   password: ''
  // };
  // loginVerify() {
  //   this._upService.verify(this.loginUser)
  //     .subscribe(result => {
  //       console.log('result is', result);
  //       localStorage.setItem('userId', result.userID);
  //       this.toastr.success('Welcome ' + this.loginUser.email + '!', 'Logged In');
  //       this._router.navigate(['/dashboard']);
  //     },
  //     (err) => {
  //       if (err.status === 404) {
  //         this.toastr.error('Incorrect Access Info!', 'Logged Failed');
  //       }
  //     });
  // }

  // Jwt token login
  errors: string;
  isRequesting: boolean;
  submitted: boolean = false;
  loginUser: Credentials = { userName: '', password: '' };

  login({ value, valid }: { value: Credentials, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors='';
    if (valid) {
      this._userService.login(value.userName, value.password)
        .finally(() => this.isRequesting = false)
        .subscribe(
        result => {         
          if (result) {
            this._router.navigate(['/dashboard']);   
          }
        },
        error => this.errors = error);
    }
  }

  forgetPassword() {
    // send random generated password to user through user to reset password
  }

  redirectToRegister() {
    this._router.navigate(['/pages/register']);
  }

  getDemoAccount() {

  }
}
