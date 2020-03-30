import {Component, OnInit} from '@angular/core';
import {Message} from "./message/message";

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {
  messages: Array<Message>;


  constructor() {
    this.messages = new Array<any>();
    this.messages.push({
      string: "WOW",
      senderId: "Huseyin",
      isOtherUser: false,
    })
    this.messages.push({
      string: "Dit is een bericht",
      senderId: "Storm",
      isOtherUser: true,
    })
    this.messages.push({
      string: "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
      senderId: "Huseyin",
      isOtherUser: false,
    })
    this.messages.push({
      string: "Lorem Ipsum is simply dummy text of the printing ",
      senderId: "Storm",
      isOtherUser: true,
    })
  }

  ngOnInit( ): void {
  }
}
