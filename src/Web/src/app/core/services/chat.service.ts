import {Injectable} from '@angular/core';
import {ApiService} from "@IQuality/core/services/api.service";
import {BaseChat} from "@IQuality/core/models/base-chat";
import * as signalR from "@microsoft/signalr";
import {LogLevel} from "@microsoft/signalr";
import {Message} from "@IQuality/core/models/message";
import {AuthenticationService} from "@IQuality/core/services/authentication.service";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  public selected: BaseChat;
  public messages: Array<Message> = [];
  public onChatSelected: Array<() => void> = [];
  private connection: signalR.HubConnection;

  constructor(private _api: ApiService, private auth: AuthenticationService) {
    // TODO: Put the HubConnection Url in the environment.
    this.connection = new signalR.HubConnectionBuilder()
      .withAutomaticReconnect()
      .withUrl(`${environment.endpoints.api}/hub`, {
      accessTokenFactory: () => auth.encodedToken
    }).configureLogging(LogLevel.Warning).build();

    // this.connection.on("messageReceived", (userId: string, chatId: string, message: string) => {
    //   if (chatId === this.selected.id) {
    //     let newMessage = new Message();
    //     newMessage.content = message;
    //
    //     if (userId === this.auth.nameIdentifier) {
    //       newMessage.senderId = userId;
    //     }
    //
    //     this.messages.push(newMessage);
    //   }
    // });

    this.connection.on("messageReceived", (message: string) => {
      console.log(message);
    });

    this.connection.start().catch(err => {
      console.log("Connection error", err);
    });

    console.log(this.connection);
  }

  public sendMessage(content: string) {
    this.connection.send("newMessage", this.auth.nameIdentifier, this.selected.id, content);
  }

  public createChat(name: string): Promise<BaseChat> {
    return this._api.post<BaseChat>('/chats', {name});
  }

  public getChats(): Promise<Array<BaseChat>> {
    return this._api.get<Array<BaseChat>>('/chats');
  }

  public async selectChatWithId(id: string): Promise<BaseChat> {
    this.selected = await this._api.get<BaseChat>(`/chats/${id}`);
    this.messages = this.selected.messages;
    this.onChatSelected.forEach(value => {
      value();
    });

    this.connection.invoke("JoinGroup", id).catch(err => {
      console.log(err)
    });

    return this.selected;
  }
}
