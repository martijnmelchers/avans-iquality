import {Component, Input, OnInit} from '@angular/core';
import {TextMessage} from "@IQuality/core/models/messages/text-message";
import {Message} from "@IQuality/core/models/messages/message";

@Component({
  selector: 'app-user-message',
  templateUrl: './user-message.component.html',
  styleUrls: ['./user-message.component.scss']
})
export class UserMessageComponent implements OnInit {
  @Input("message") message: Message;
  @Input("isCurrentUser") isCurrentUser: boolean;

  public textMessage: TextMessage

  constructor() {}

  ngOnInit(): void {
    this.textMessage = this.message as TextMessage
  }

}
