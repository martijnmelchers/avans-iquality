import {Injectable} from '@angular/core';
import {ApiService} from "@IQuality/core/services/api.service";
import {BaseChat} from "@IQuality/core/models/base-chat";
import * as signalR from "@microsoft/signalr";
import {LogLevel} from "@microsoft/signalr";
import {Message} from "@IQuality/core/models/message";
import {AuthenticationService} from "@IQuality/core/services/authentication.service";
import {environment} from "../../../environments/environment";
import {PatientMessage} from "@IQuality/core/models/patient-message";
import {formatDate} from "@angular/common";

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  public chatWithBot: boolean;
  public isChatWithBot() { return this.chatWithBot; }

  public selected: BaseChat;
  public messages: Array<Message> = [];
  public onChatSelected: Array<() => void> = [];

  private readonly connection: signalR.HubConnection;

  constructor(private _api: ApiService, private auth: AuthenticationService) {
    this.chatWithBot = true;
    // TODO: Put the HubConnection Url in the environment.
    this.connection = new signalR.HubConnectionBuilder()
      .withAutomaticReconnect()
      .withUrl(`${environment.endpoints.api}/hub`, {
        accessTokenFactory: () => auth.encodedToken
      }).configureLogging(LogLevel.Warning).build();

    this.connection.on("messageReceived", (userId: string, userName: string, chatId: string, message: string) => {
      if (chatId === this.selected.id) {
        let newMessage = new Message();

        newMessage.content = message;
        newMessage.senderId = userId;
        newMessage.senderName = userName;
        newMessage.sendDate = new Date(Date.now())


        this.messages.push(newMessage);
      }
    });

    this.connection.start().then(() => {
      const response = this.getChats();
      response.then((chats) => {
        for (const chat of chats) {
          this.hubJoinGroup(chat.id);
        }
      })
    }).catch(err => {
      console.log("Connection error", err);
    });
  }

  public sendMessage(content: string) {
    if(this.chatWithBot) {
      const patientMessage = new PatientMessage();
      patientMessage.roomId = this.selected.id;
      patientMessage.text = content;

      this._api.post<any>("/dialogflow/patient",  patientMessage).then((response)=> {
        let userMessage = new Message();
        userMessage.senderId = this.auth.getNameIdentifier;
        userMessage.senderName = this.auth.getName;
        userMessage.content = content;
        this.messages.push(userMessage);

        let botMessage = new Message();
        if(response != null){
          botMessage.content = response.fulfillmentText;
          this.messages.push(botMessage);
        }
      })
    } else {
      this.connection.send("newMessage", this.selected.id, content);
    }
  }

  public async createBuddychat(name: string): Promise<BaseChat> {
    let chat = await this._api.post<BaseChat>('/chats/createbuddychat', {name});
    this.hubJoinGroup(chat.id);
    return chat;
  }

  public async getChats(): Promise<Array<BaseChat>> {
    return await this._api.get<Array<BaseChat>>('/chats');
  }

  public async selectChatWithId(id: string): Promise<BaseChat> {
    this.selected = await this._api.get<BaseChat>(`/chats/${id}`);

    this.messages = this.selected.messages;
    this.onChatSelected.forEach(value => {
      value();
    });

    return this.selected;
  }

  private hubJoinGroup(roomId: string) {
    this.connection.invoke("JoinGroup", roomId).catch(err => {
      console.log(err)
    });
  }

  public getTime(date: string) : string {
    const time = new Date(date);
    return `${time.getHours()}:${time.getMinutes()}`
  }
}
