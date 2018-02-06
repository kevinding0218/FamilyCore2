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
  loginUser: RegisterInfo = {
    email: '',
    password: ''
  };

  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _upService: UserPasswordService,
    private toastr: ToastrService) { }

  loginVerify() {
    this._upService.verify(this.loginUser)
      .subscribe(result => {
        console.log('result is', result);
        localStorage.setItem('userId', result.userID);
        this.toastr.success('Welcome ' + this.loginUser.email + '!', 'Logged In');
        this._router.navigate(['/dashboard']);
      },
      (err) => {
        if (err.status === 404) {
          this.toastr.error('Incorrect Access Info!', 'Logged Failed');
        }
      });
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
