import { ActionType } from './ActionType';

export class Action{

  public id : string;
  public chatId: string;
  public goalId: string;
  public type: ActionType;
  public description: string;
  public lastReminder: string;

}
