import { Injectable } from '@angular/core';
import { ApiService } from "@IQuality/core/services/api.service";
import { CookieService } from "ngx-cookie-service";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private readonly _api: ApiService, private _cookie: CookieService) {
  }


  public async login(username: string, password: string): Promise<void> {
    var result = await this._api.post<string>("authorize/login", { username, password });
    this._cookie.set("jwtToken", result);
  }
}
