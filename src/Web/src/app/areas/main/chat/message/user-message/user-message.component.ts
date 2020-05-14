import {Component, Input, OnInit} from '@angular/core';
import {TextMessage} from "@IQuality/core/models/messages/text-message";

@Component({
  selector: 'app-user-message',
  templateUrl: './user-message.component.html',
  styleUrls: ['./user-message.component.scss']
})
export class UserMessageComponent implements OnInit {
  @Input("message") message: TextMessage;
  @Input("isCurrentUser") isCurrentUser: boolean;

  constructor() { }

  ngOnInit(): void {
  }

}
