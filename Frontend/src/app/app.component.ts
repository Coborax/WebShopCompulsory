import { Component } from '@angular/core';
import {UserDto} from "./auth/shared/user.dto";
import {AuthService} from "./auth/shared/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'petshop-frontend';
  user: UserDto | undefined;

  constructor(private authService: AuthService,
              private router: Router) {
  }

  ngDoCheck(): void {
    const userInfo = this.authService.getUserObject();
    if (userInfo?.user) {
      this.user = userInfo.user
    }
  }

  logout(): void {
    this.authService.logout();
    this.user = undefined;
    this.router.navigateByUrl('auth/login');
  }
}
