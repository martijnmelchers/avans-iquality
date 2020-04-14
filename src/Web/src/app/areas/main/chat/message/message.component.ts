import {Component, Input, OnInit} from '@angular/core';
import {Message} from "@IQuality/core/models/message";
import {AuthenticationService} from "@IQuality/core/services/authentication.service";

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss']
})
export class MessageComponent implements OnInit {
  @Input("message") message: Message;

  constructor(public auth: AuthenticationService) { }

  ngOnInit(): void {}

}
