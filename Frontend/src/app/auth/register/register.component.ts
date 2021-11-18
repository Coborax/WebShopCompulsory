import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../shared/auth.service";
import {Router} from "@angular/router";
import {LoginUser} from "../shared/login-user.model";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {


  registerForm: FormGroup;

  constructor(private _auth: AuthService,
              private router: Router) {
    this.registerForm = new FormGroup({
      username: new FormControl(
        '',
        [
          Validators.required,
          Validators.minLength(2)
        ]
      ),
      password: new FormControl(
        '',
        Validators.required
      ),
    })
  }

  ngOnInit(): void {
  }

  register() {
    if(this.registerForm.valid) {
      let userLogin = this.registerForm.value as LoginUser;
      console.log('Login info', userLogin);
      this._auth.register(userLogin)
        .subscribe(_ => {
          console.log("User registered!");
          this.router.navigateByUrl('auth/login');
        });
    }
  }

  get username() {return this.registerForm.get('username')}
  get password() {return this.registerForm.get('password')}

}
