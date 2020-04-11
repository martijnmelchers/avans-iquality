import {Injectable} from '@angular/core';
import {ApiService} from "@IQuality/core/services/api.service";
import {BaseChat} from "@IQuality/core/models/base-chat";
import * as signalR from "@microsoft/signalr";
import {Message} from "@IQuality/core/models/message";
import {throwError} from "rxjs";
import {AuthenticationService} from "@IQuality/core/services/authentication.service";
import {environment} from "../../../environments/environment";
import {DialogflowResult} from "@IQuality/core/models/dialogflow-result";

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private chatWithBot: boolean;
  public selected: BaseChat;
  public messages: Array<Message> = [];
  public onChatSelected: Array<() => void> = [];
  private connection: any;
  private latestDialogflowResponse: DialogflowResult;

  constructor(private _api: ApiService, private auth: AuthenticationService) {
    this.chatWithBot = true;
    // TODO: Put the HubConnection Url in the environment.
    this.connection = new signalR.HubConnectionBuilder().withUrl(`${environment.endpoints.api}/hub`).build();

    this.connection.on("messageReceived", (userId: string, chatId: string, message: string) => {
      if (chatId === this.selected.id) {
        let newMessage = new Message();
        newMessage.content = message;

        if (userId === this.auth.nameIdentifier) {
          newMessage.senderId = userId;
        }


        this.messages.push(newMessage);
      }
    });

    this.connection.start().catch(err => {
      throwError(err);
    });
    this.selected = new BaseChat();
    this.selected.name = "test";
  }

  public sendMessage(content: string) {
    if(this.chatWithBot) {
      this._api.post<DialogflowResult>("/dialogflow/patient", {text: content, response: this.latestDialogflowResponse}).then((response)=> {
        let userMessage = new Message();
        userMessage.senderId = this.auth.nameIdentifier;
        userMessage.content = content;
        this.messages.push(userMessage);

        this.latestDialogflowResponse = response;
        let botMessage = new Message();
        if(response != null){
          botMessage.content = response.fulfillmentText;
          this.messages.push(botMessage);
        }

      })
    } else {
      this.connection.send("newMessage", this.auth.nameIdentifier, this.selected.id, content);
    }
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

    return this.selected;
  }
}
