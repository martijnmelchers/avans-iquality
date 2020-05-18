import { Injectable } from '@angular/core';
import {ApiService} from "@IQuality/core/services/api.service";
import {ApplicationUser} from "@IQuality/core/models/application-user";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private _api: ApiService) { }

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
