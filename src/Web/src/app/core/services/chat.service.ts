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

  //Messages zijn voor alles om te laten zien
  public messages: Array<Message> = [];
  //Database messages zijn de messages die opgeslagen zijn in de database
  public databaseMessages: Array<Message> = [];

  public onChatSelected: Array<() => void> = [];

  private connection: signalR.HubConnection;

  constructor(private _api: ApiService, private auth: AuthenticationService) {
    this.setUpSocketConnection(auth)
  }

  public sendMessage(content: string) {
    if(this.chatWithBot) {
      const patientMessage = new PatientMessage();
      patientMessage.roomId = this.selected.id;
      patientMessage.text = content;

      this._api.post<any>("/dialogflow/patient",  patientMessage).then((response)=> {
        this.messages.push(this.createMessage(content));

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
    this.chatWithBot = false;
    this.selected = await this._api.get<BaseChat>(`/chats/${id}`);

    this.messages = this.databaseMessages = this.selected.messages;
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

  private createMessage(content: string){
    let message = new Message();
    message.senderId = this.auth.getNameIdentifier;
    message.senderName = this.auth.getName;
    message.content = content;
    message.sendDate = new Date(Date.now());

    return message
  }

  private setUpSocketConnection(auth: AuthenticationService){
    this.connection = new signalR.HubConnectionBuilder()
      .withAutomaticReconnect()
      .withUrl(`${environment.endpoints.api}/hub`, {
        accessTokenFactory: () => auth.encodedToken
      }).configureLogging(LogLevel.Warning).build();

    this.connection.on("messageReceived", (userId: string, userName: string, chatId: string, content: string) => {
      if (chatId === this.selected.id) {
        this.databaseMessages.push(this.createMessage(content));
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

}
