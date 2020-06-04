import {ApiService} from "@IQuality/core/services/api.service";
import {Goal} from "@IQuality/core/models/goal";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class GoalService{

  constructor(private _api: ApiService) { }

  public async getGoalsFromUser(userId: string) : Promise<[Goal]> {
    console.log(userId);
    return await this._api.get<[Goal]>(`/goals`)
  }
  public async getActionsFromUser(applicationUserId: string){

  }
  public  removeGoalFromUser(applicationUserId: string, goalId: string){
  }

  public  removeActionFromUser(applicationUserId: string, goalId: string){
  }
}

