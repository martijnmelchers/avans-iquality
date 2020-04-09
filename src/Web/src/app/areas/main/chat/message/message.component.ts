import {Component, Input, OnInit} from '@angular/core';
import {Message} from "@IQuality/core/models/message";

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss']
})
export class MessageComponent implements OnInit {
  @Input("message") message: Message;

  constructor() { }

  ngOnInit(): void {
  }

}
