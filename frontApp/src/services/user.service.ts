import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonService, Result } from './common.service';
import { Observable, Subject } from 'rxjs';
import { JwtPayload, jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient, private common: CommonService) {}
  url: string = '';

  decodedToken: any;

  logout(): void {
    localStorage.removeItem('jwt-token');
  }

  Login(login: Login): Observable<GetLoginResult> {
    this.url = this.common.baseUrl + 'Users/Login';
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<GetLoginResult>(this.url, login, { headers });
  }

  GetUsers(): Observable<GetUsersResult> {
    this.url = this.common.baseUrl + 'Users/GetAllUsers';
    return this.http.get<GetUsersResult>(this.url);
  }

  GetTokenFromLocalStorage(): string {
    let token: string = '';
    if (typeof localStorage !== 'undefined') {
      token = localStorage.getItem('jwt-token');
    }

    return token;
  }

  SetItemInLocalStorage(token: string) {
    localStorage.setItem('jwt-token', token);
  }

  GetUserRoleId(token: string): JwtPayload {
    const decoded = jwtDecode(token);

    return decoded;
  }

  GetRoleId(): number {
    const token = this.GetTokenFromLocalStorage();
    if (token) {
      const decoded = jwtDecode(token);
      this.decodedToken = decoded;
      const roleId = parseInt(this.decodedToken.Role);
      console.log(roleId);
      return roleId;
    }
    return null;
  }
}

export class GetUsersResult extends Result {
  override MyResult: User[];
}
export class GetLoginResult extends Result {
  override MyResult: User;
}

export class User {
  USERID: number;
  ROLEID: number;
  USERNAME: string;
  PASSWORD: string;
  ROLENAME: string;
  Token: string;
}

export class Login {
  USERNAME: string;
  PASSWORD: string;
}
