import {Component, OnInit} from '@angular/core';
import {ChatService} from "@IQuality/core/services/chat.service";
import {ChatContext} from "@IQuality/core/models/chat-context";
import {NotificationService} from "carbon-components-angular";
import {Message} from "@IQuality/core/models/messages/message";

@Component({
  selector: 'app-chat-list',
  templateUrl: './chat-list.component.html',
  styleUrls: ['./chat-list.component.scss'],
  providers: []
})
export class ChatListComponent implements OnInit {
  chats: Array<ChatContext> = [];
  chatName: string;

  constructor(public chatService: ChatService) {
  }

  ngOnInit(): void {
    this.chatService.getChats().then((response) => {
      this.chats = response;

    }, err => console.log(err));
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
        if(a > b){
          return -1;
        }
        if(a < b){
          return 1;
        }
        return 0;
      });


      return chat.messages[0];
    }

}
