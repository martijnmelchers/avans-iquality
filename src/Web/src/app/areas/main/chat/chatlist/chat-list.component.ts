import { ApiService } from '@IQuality/core/services/api.service';
import { Tip } from '@IQuality/core/models/tip';
import {Component, OnInit} from '@angular/core';
import {ChatService} from "@IQuality/core/services/chat.service";
import {ChatContext} from "@IQuality/core/models/chat-context";
import { TipService } from '@IQuality/core/services/tip.service';
import { AuthenticationService } from '@IQuality/core/services/authentication.service';

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

  constructor(public chatService: ChatService, private _api: ApiService, private _tipService: TipService, private _authService: AuthenticationService) {
    this.chatService.getChats().then((response) => {

      if(response != null)
      {

        response.forEach(e => {
          if(this.chatService.isBuddyChat)
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
  }

  //TODO: Verplaatsen
  async ngOnInit(): Promise<void> {

    await this._tipService.getRandomTip().then((response) => {
      if (response.id !== null)
        this.notification = response;
    });

    if (this.notification.id == null) {
      this.notification.name = "Iquality";
      this.notification.description = "Welcome to DiaBuddy!"
    }

    if (this._authService.getRole == 'patient') {
      this.setOneSignalSettings();
    }

  }

  setOneSignalSettings() {
    let thisComponent = this;
    let OneSignal = window['OneSignal'] || [];
    OneSignal.push(function() {
      OneSignal.init({
        appId: "83238485-a09c-4593-ae5b-0281f6495b79",
        promptOptions: {
          /* These prompt options values configure both the HTTP prompt and the HTTP popup. */
          /* actionMessage limited to 90 characters */
          actionMessage: "We'd like to show you notifications for the latest news and updates.",
          /* acceptButtonText limited to 15 characters */
          acceptButtonText: "ALLOW",
          /* cancelButtonText limited to 15 characters */
          cancelButtonText: "NO THANKS"
        },
        notifyButton: {
          enable: false,
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

    OneSignal.push(() => {
      OneSignal.showSlidedownPrompt();
      // Occurs when the user's subscription changes to a new value.
      OneSignal.on('subscriptionChange',  (isSubscribed) => {
        OneSignal.getUserId().then(id => {
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
