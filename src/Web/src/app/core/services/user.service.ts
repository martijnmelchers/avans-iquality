import { Injectable } from '@angular/core';
import {ApiService} from "@IQuality/core/services/api.service";
import {ApplicationUser} from "@IQuality/core/models/application-user";
import {AuthenticationService} from "@IQuality/core/services/authentication.service";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  public async firstTime(): Promise<boolean> {
    if (this._user === null) {
      this._user = await this.getUser(this.authService.getNameIdentifier);
    }
    return this._user.firstTime;
  }

  private _user: ApplicationUser = null;

  constructor(private _api: ApiService, private authService: AuthenticationService) {
  }

  getUsers(){
    return this._api.get<Array<ApplicationUser>>('/users', {});
  }

  getUser(applicationUserId: string){
    return this._api.get<ApplicationUser>(`/users/${applicationUserId}`, {});
  }

  // This will prob. "deactivate" the account.
  public deactivateUser(applicationUserId: string){
    return this._api.put(`/users/${applicationUserId}`, {});
  }

  public deleteUser(applicationUserId: string){
    return this._api.delete(`/users/${applicationUserId}`);
  }
}
