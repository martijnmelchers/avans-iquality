import { Component, OnInit } from '@angular/core';
import { ChatService } from "@IQuality/core/services/chat.service";
import { ChatContext } from "@IQuality/core/models/chat-context";
import { NotificationService } from "carbon-components-angular";
import { Message } from "@IQuality/core/models/messages/message";
import { ApiService } from '@IQuality/core/services/api.service';
import { Tip } from '@IQuality/core/models/tip';

@Component({
  selector: 'app-chat-list',
  templateUrl: './chat-list.component.html',
  styleUrls: ['./chat-list.component.scss'],
  providers: []
})
export class ChatListComponent implements OnInit {
  chats: Array<ChatContext> = [];
  chatName: string;
  notification: any = {};

  constructor(public chatService: ChatService, private _api: ApiService) {
  }

  //TODO: Verplaatsen
  async ngOnInit(): Promise<void> {
    this.chatService.getChats().then((response) => {
      this.chats = response;

    }, err => console.log(err));

    // await this._api.get<any>('/tip/getrandomtip').then(resp => {
    //   this.notification = resp;

    // });
    let thisComponent = this;
    let OneSignal = window['OneSignal'] || [];
    OneSignal.push( () => {
      console.log('Register For Push');
      OneSignal.push(["registerForPushNotifications"])
    });
    OneSignal.push( () => {
      console.log("firstt");
      // Occurs when the user's subscription changes to a new value.
      OneSignal.on('subscriptionChange',  (isSubscribed) => {
        console.log("The user's subscription state is now:", isSubscribed);
        OneSignal.getUserId().then(id => {
          console.log('resp: ',id);
          thisComponent._api.post<any>(`/patient/${id}/${isSubscribed}`,{});
        });
        });
    });
      
  }

  onBuddyChatCreate(isBuddyChat: boolean) {
    if (this.chatName) {
      this.chatService.createBuddychat(this.chatName, isBuddyChat).then((response) => {
        this.chats.push(response);
      });
    }
  }

  getLastMessage(chat: ChatContext): any {
    chat.messages.sort((a, b) => {
      if (a > b) {
        return -1;
      }
      if (a < b) {
        return 1;
      }
      return 0;
    });


    return chat.messages[0];
  }

}
