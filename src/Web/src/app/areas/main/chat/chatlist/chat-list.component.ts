import {Component, OnInit} from '@angular/core';
import {BaseChat} from "@IQuality/core/models/base-chat";
import {ChatService} from "@IQuality/core/services/chat.service";

@Component({
  selector: 'app-chat-list',
  templateUrl: './chat-list.component.html',
  styleUrls: ['./chat-list.component.scss']
})
export class ChatListComponent implements OnInit {
  chats: Array<BaseChat>;
  chatName: string;

  constructor(public chatService: ChatService) {
  }

  ngOnInit(): void {
    this.chatService.getChats().then((response) => {
      this.chats = response;
    });
  }

  onChatCreate() {
    if (this.chatName) {
      this.chatService.createBuddychat(this.chatName).then((response) => {
        this.chats.push(response);
      });
    }
  }
}
