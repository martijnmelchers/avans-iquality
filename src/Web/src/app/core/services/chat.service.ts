import {Injectable} from '@angular/core';
import {ApiService} from "@IQuality/core/services/api.service";
import {BaseChat} from "@IQuality/core/models/base-chat";
import {Message} from "@IQuality/areas/main/chat/message/message";

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  public selected: BaseChat;
  public messages: Array<Message>;

  public onChatSelected: Array<() => void> = [];

  constructor(private _api: ApiService) {

  }

  public createChat(name: string): Promise<BaseChat> {
    return this._api.post<BaseChat>('/chats', {name});
  }

  public getChats(): Promise<Array<BaseChat>> {
    return this._api.get<Array<BaseChat>>('/chats');
  }

  public async selectChat(id: string) : Promise<BaseChat> {
    this.selected = await this._api.get<BaseChat>(`/chats/${id}`);
    this.messages = await this._api.get<Array<Message>>(`/chats/${id}/messages`);

    this.onChatSelected.forEach(value => {
      value();
    });

    return this.selected;
  }
}
