import {Component, Input, OnInit} from '@angular/core';
import {AuthenticationService} from "@IQuality/core/services/authentication.service";
import {Suggestion} from "@IQuality/core/models/suggestion";
import {ChatService} from "@IQuality/core/services/chat.service";
import {TextMessage} from "@IQuality/core/models/messages/text-message";
import {Listable} from "@IQuality/core/models/listable";

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss']
})
export class MessageComponent implements OnInit {

  @Input("message") message: TextMessage;
  constructor(public auth: AuthenticationService) { }

  ngOnInit(): void {

  }

}
