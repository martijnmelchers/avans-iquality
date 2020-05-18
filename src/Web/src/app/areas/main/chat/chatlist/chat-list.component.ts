import { ApiService } from '@IQuality/core/services/api.service';
import { Tip } from '@IQuality/core/models/tip';
import {Component, OnInit} from '@angular/core';
import {ChatService} from "@IQuality/core/services/chat.service";
import {ChatContext} from "@IQuality/core/models/chat-context";
import {NotificationService} from "carbon-components-angular";
import {Message} from "@IQuality/core/models/messages/message";
import {DEBUG} from "@angular/compiler-cli/ngcc/src/logging/console_logger";

@Component({
  selector: 'app-chat-list',
  templateUrl: './chat-list.component.html',
  styleUrls: ['./chat-list.component.scss'],
  providers: []
})
export class ChatListComponent implements OnInit {
  notification: any = {};
  buddyChats: Array<ChatContext> = [];
  patientChats: Array<ChatContext> = [];


  filteredPatientChats: Array<ChatContext> = [];
  filteredBuddyChats: Array<ChatContext> = [];

  searchChatName: string;
  createChatName: string;

  constructor(public chatService: ChatService, private _api: ApiService) {
  }

  //TODO: Verplaatsen
  async ngOnInit(): Promise<void> {
    this.chatService.getChats().then((response) => {
      if(response != null)
      {

        console.log(response);
        response.forEach(e => {
          if(e.chat.type === "BuddyChat")
          {
            this.buddyChats.push(e);
          }
          else
          {
            this.chatService.getContactName(e.chat.id).then((response: string) => {
              e.contactName = response;
            });

            this.patientChats.push(e);
          }
        })


        this.filteredPatientChats = this.patientChats;
        this.filteredBuddyChats = this.buddyChats;
      }
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

  onChatCreate(isBuddyChat: boolean) {
    if (this.createChatName) {
      this.chatService.createBuddychat(this.createChatName, isBuddyChat).then((response) => {
        this.patientChats.push(response);
      });
    }
  }

  getLastMessage(chat: ChatContext): any {
    if (chat.messages === null) {
      return null;
    }

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

  searchValueChange($event: string) {
    this.searchChatName = $event;
    this.filteredPatientChats = this.patientChats.filter(i => {
      return i.chat.name.toLowerCase().indexOf(this.searchChatName.toLowerCase()) >= 0;
    });

    this.filteredBuddyChats = this.buddyChats.filter(i => {
      return i.chat.name.toLowerCase().indexOf(this.searchChatName.toLowerCase()) >= 0;
    });
  }

  searchClear() {
    this.searchChatName = '';
  }
}
