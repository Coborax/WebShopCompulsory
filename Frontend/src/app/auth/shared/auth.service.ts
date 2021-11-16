import { Injectable } from '@angular/core';
import {LoginUser} from './login-user.model';
import {Observable, of} from 'rxjs';
import {HttpClient} from "@angular/common/http";
import {TokenDto} from "./token.dto";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  login(user: LoginUser): Observable<TokenDto> {
    return this.http.post<TokenDto>("https://localhost:5001/api/Auth/Login", user);
  }

  register(user: LoginUser): Observable<string> {
    return this.http.post<string>("https://localhost:5001/api/Auth/Register", user);
  }

  getToken(): string | null {
    const user = this.getUserObject();
    if (user) {
      return user.jwtToken;
    } else {
      return null;
    }
  }

  getUsername(): string | null {
    const user = this.getUserObject();
    if (user) {
      return user.user.username;
    } else {
      return null;
    }
  }

  getUserObject(): TokenDto | null {
    const currentUser = JSON.parse(<string>localStorage.getItem('currentUser'));
    if (currentUser) {
      return currentUser;
    } else {
      return null;
    }
  }

  logout() {
    localStorage.removeItem('currentUser');
  }
}
