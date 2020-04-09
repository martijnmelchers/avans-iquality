import {Injectable} from '@angular/core';
import {ApiService} from "@IQuality/core/services/api.service";
import {BaseChat} from "@IQuality/core/models/base-chat";

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor(private _api: ApiService) {

  }

  public createChat(name: string): Promise<BaseChat> {
    return this._api.post<BaseChat>('/chats', {name});
  }
}
