import { EventEmitter, Injectable } from '@angular/core';
import {ApiService} from '@IQuality/core/services/api.service';
import {BaseChat} from '@IQuality/core/models/base-chat';
import * as signalR from '@microsoft/signalr';
import {LogLevel} from '@microsoft/signalr';

import {Message} from '@IQuality/core/models/messages/message';
import {TextMessage} from '@IQuality/core/models/messages/text-message';

import {AuthenticationService} from "@IQuality/core/services/authentication.service";
import {environment} from "../../../environments/environment";
import {BotMessage} from "@IQuality/core/models/messages/bot-message";
import {ChatContext} from "@IQuality/core/models/chat-context";
import {DEBUG} from "@angular/compiler-cli/ngcc/src/logging/console_logger";
import {NotificationService} from "carbon-components-angular";
import { BehaviorSubject, Observable } from "rxjs";
import {Listable} from "@IQuality/core/models/listable";
import {TutorialService} from "@IQuality/core/services/tutorial.service";
import {UserService} from "@IQuality/core/services/user.service";


@Injectable({
  providedIn: 'root'
})
export class ChatService {
  constructor(private _api: ApiService, private _auth: AuthenticationService, private _notificationService: NotificationService,
              private tutorialService: TutorialService, private userService: UserService) {
    this._auth.SetChatService = this;
  }

  private auth: AuthenticationService;
  private connection: signalR.HubConnection;

  public chatWithBot: boolean;
  public selected: ChatContext;

  // Messages zijn voor alles om te laten zien
  public messages: Array<Message> = [];

  public messageSubject: EventEmitter<void> = new EventEmitter<void>(false);

  public onChatSelected: Array<() => void> = [];

  private _chats: Array<ChatContext>;



  private static createMessage(userId: string, userName: string, chatId: string, content: string) {
    const message = new TextMessage();
    message.chatId = chatId;
    message.senderId = userId;
    message.senderName = userName;
    message.content = content;
    message.type = 'TextMessage';
    message.sendDate = new Date(Date.now());

    return message;
  }

  public connectWithChats() {
    this.setUpSocketConnection(this._auth);
  }

  public async sendMessage(content: string) {
    await this.connection.send('newMessage', this.selected.chat.id, content);
    if (this.chatWithBot) {

      const patientMessage = new TextMessage();
      patientMessage.chatId = this.selected.chat.id;
      patientMessage.content = content;

      const response = await this._api.post<BotMessage>('/dialogflow/patient', patientMessage, null, {disableRequestLoader: true});
      this.messages.push(response);
      this.messageSubject.next()

      if(await this.userService.firstTime()){
        switch(response.content){
          case "Awesome! Goal has been created! Try adding some actions to reach your goal!":
          case "I created a new action!":
          case "Your weight is duly noted!":
          case "Sure, here is your progress":
            this.tutorialService.nextStage();
            this.postTutorialMessage();
            break;
        }
      }
    }
  }

  public async createBuddychat(name: string, isBuddyChat: boolean): Promise<ChatContext> {
    let chat;

    if (isBuddyChat) {
      chat = await this._api.post<BaseChat>('/buddy', {name});
    } else {
      chat = await this._api.post<BaseChat>('/chats', {name});
    }

    this.hubJoinGroup(chat.id);
    return chat;
  }

  public async getChats(): Promise<Array<ChatContext>> {
    return await this._api.get<Array<ChatContext>>('/chats');
  }

  public async selectChatWithId(id: string): Promise<ChatContext> {
    this.chatWithBot = false;
    this.selected = await this._api.get<ChatContext>(`/chats/${id}`);
    this.messages = this.selected.messages.reverse();

    if(await this.userService.firstTime())
      this.postTutorialMessage();

    this.messageSubject.next();

    this.onChatSelected.forEach(value => {
      value();
    });
    return this.selected;
  }

  private hubJoinGroup(roomId: string) {
    if (!this.connection) {
      return;
    }

    this.connection.invoke('JoinGroup', roomId).catch(err => {
      (err);
    });
  }

  public getTime(date: string): string {
    const time = new Date(date);
    return `${time.getHours()}:${time.getMinutes()}`;
  }



  private setUpSocketConnection(auth: AuthenticationService) {
    this.disconnect();
    this.connection = new signalR.HubConnectionBuilder()
      .withAutomaticReconnect()
      .withUrl(`${environment.endpoints.api}/hub`, {
        accessTokenFactory: () => auth.encodedToken
      }).build();

    this.connection.onreconnecting(() => console.log('Chat is reconnecting...'));
    this.connection.onreconnected(() => console.log('Chat is reconnected!'));
    this.connection.onclose(() => console.log('Connection closed!'));


    this.connection.on("messageReceived", async (userId: string, userName: string, chatId: string, content: string) => {
      if(this.selected){
        if (chatId === this.selected.chat.id) {
          const message = ChatService.createMessage(userId, userName, chatId, content);
          this.messages.push(message);
          this.messageSubject.next();
        }
      }
    });

    this.connection.start().then(() => {
      const response = this.getChats();
      response.then((chats) => {

        this._chats = chats;
        for (const context of chats) {
          this.hubJoinGroup(context.chat.id);
        }
      });
    }).catch(err => {
      console.log('Connection error', err);
    });
  }

  public getChatObservable(): Observable<any> {
    return new Observable<any>((observer) => {
      this.connection.on('messageReceived', (userId: string, userName: string, chatId: string, content: string) => {
        const chat = this._chats.find((chat) => chat.chat.id === chatId);
        const  message = {
          senderId: userId,
          userName,
          chatId,
          content,
          chatName: chat.chat.name
        };
        observer.next(message);
      });
    });
  }

  deleteGoal(message: BotMessage, data: Listable) {
    this._api.delete(`/dialogflow/goal/${data.id}`).then(() => {
      const index = message.listData.indexOf(data, 0);
      if (index > -1) {
        message.listData.splice(index, 1);
      }
    });
  }

  getContactName(chatId: string) {
    return this._api.get<string>(`/chats/${chatId}/contact`, null, {responseType: 'text'}).catch(e => {
      return 'No Contact';
    });
  }

  public disconnect() {
    if (!this.connection) {
      return;
    }

    this.connection.stop();
    this.connection = null;
  }

  public hasConnection(): boolean {
    return this.connection != null;
  }

  public getRole() {
    return this._auth.getRole;
  }

  public postTutorialMessage() {
    this.messages.push(this.tutorialService.getTutorialMessage(this.selected.chat.id));
    this.messageSubject.next();
  }
}
