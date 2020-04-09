import {Component, OnInit} from '@angular/core';
import {BaseChat} from "@IQuality/core/models/base-chat";
import {HttpClient} from "@angular/common/http";
import {ChatService} from "@IQuality/core/services/chat.service";

@Component({
  selector: 'app-chatlist',
  templateUrl: './chatlist.component.html',
  styleUrls: ['./chatlist.component.scss']
})
export class ChatlistComponent implements OnInit {
  chats: Array<BaseChat>;

  constructor(private chatService: ChatService) {
  }

  ngOnInit(): void {
  }

  selected($event: { value: string; selected: boolean; name: string }) {
    console.log($event);
  }

  onChatCreate() {
    this.chatService.createChat("test").then((response) => {
      console.log(response);
    });
  }
}
