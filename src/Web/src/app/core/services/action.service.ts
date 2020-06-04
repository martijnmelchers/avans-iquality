import {ApiService} from "@IQuality/core/services/api.service";
import {Goal} from "@IQuality/core/models/goal";
import {Action} from "@IQuality/core/models/Action";

import {Injectable} from "@angular/core";
import { ActionType } from '../models/ActionType';

@Injectable({
  providedIn: 'root'
})
export class ActionService{

  constructor(private _api: ApiService) { }

  public async getActionsFromUser(userId: string) : Promise<[Action]> {
    return await this._api.get<[Action]>(`/actions`)
  }
  public  removeActionFromUser(applicationUserId: string, goalId: string){
  }

  public async getActionTypes() : Promise<[string]> {
    return await this._api.get<[string]>('/actiontypes')
  }

}
