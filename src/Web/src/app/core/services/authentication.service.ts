import { Injectable } from '@angular/core';
import { ApiService } from "@IQuality/core/services/api.service";
import { CookieService } from "ngx-cookie-service";
import { JwtHelperService } from "@auth0/angular-jwt";
import {Invite} from "@IQuality/core/models/invite";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private tokenService: JwtHelperService;

  public encodedToken: string;
  public decodedToken: any;


  constructor(private readonly _api: ApiService, private _cookie: CookieService) {
    this.tokenService = new JwtHelperService();
    this.encodedToken = this._cookie.get('token');
    console.log(this.encodedToken);
  }


  public saveToken(token: string) {
    this._cookie.set('token', token);
    this.encodedToken = token;
    this.decodedToken = this.tokenService.decodeToken(token);
  }

  public get loggedIn(): boolean {
    return this.encodedToken && this.tokenService.isTokenExpired(this.encodedToken);
  }


  async createInviteLink(): Promise<Invite> {
    return this._api.post<Invite>('/authorize/invite', {});
  }
}
