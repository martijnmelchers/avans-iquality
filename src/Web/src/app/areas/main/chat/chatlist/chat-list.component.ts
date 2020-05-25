import { ApiService } from '@IQuality/core/services/api.service';
import { Tip } from '@IQuality/core/models/tip';
import {Component, OnInit} from '@angular/core';
import {ChatService} from "@IQuality/core/services/chat.service";
import {ChatContext} from "@IQuality/core/models/chat-context";
import {NotificationService} from "carbon-components-angular";
import {Message} from "@IQuality/core/models/messages/message";
import {DEBUG} from "@angular/compiler-cli/ngcc/src/logging/console_logger";
import { TipService } from '@IQuality/core/services/tip.service';

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

  constructor(public chatService: ChatService, private _api: ApiService, private _tipService: TipService) {
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

    await this._tipService.getRandomTip().then((response) => {
      if (response.id !== null)
        this.notification = response;
    });

    if (this.notification.id == null || this.notification.id === undefined) {
      this.notification.name = "Iquality";
      this.notification.description = "Welcome to DiaBuddy!"
    }

    let thisComponent = this;
    let OneSignal = window['OneSignal'] || [];
    OneSignal.push(function() {
      OneSignal.init({
        appId: "83238485-a09c-4593-ae5b-0281f6495b79",
        promptOptions: {
          native: {
            enabled: true,
            autoPrompt: true,
            timeDelay: 5,
            pageViews: 2
          }
        },
        notifyButton: {
          enable: true,
          showCredit: false,
          displayPredicate: function() {
            return OneSignal.isPushNotificationsEnabled()
                .then(function(isPushEnabled) {
                    return !isPushEnabled;
                });
          },
        },
        subdomainName: "iqualitydomain",
      });
    });
    OneSignal.push( () => {
      OneSignal.showSlidedownPrompt();
      // Occurs when the user's subscription changes to a new value.
      OneSignal.on('subscriptionChange',  (isSubscribed) => {
        OneSignal.getUserId().then(id => {
        thisComponent._api.post<any>(`/patient/${id}/${isSubscribed}`,{});
        });
      });
    });

    await this._api.get<any>('/action').then(resp => {
      console.log(resp);
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
