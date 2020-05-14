import {Component, Input, OnInit} from '@angular/core';
import {TextMessage} from "@IQuality/core/models/messages/text-message";
import {Suggestion} from "@IQuality/core/models/suggestion";
import {Listable} from "@IQuality/core/models/listable";
import {ChatService} from "@IQuality/core/services/chat.service";

@Component({
  selector: 'app-bot-message',
  templateUrl: './bot-message.component.html',
  styleUrls: ['./bot-message.component.scss']
})
export class BotMessageComponent implements OnInit {

  @Input("message") message: TextMessage
  public suggestions = Array<Suggestion>();

  constructor(public chatService : ChatService) { }

  ngOnInit(): void {
    this.suggestions.push(new Suggestion("Show active goals", "Get goals"));
    this.suggestions.push(new Suggestion("Set goal", "Set goal"));
  }

  public onSuggestionClicked(suggestion : Suggestion) {
    this.chatService.sendMessage(suggestion.value);
  }

  onClickDelete(message: TextMessage, data: Listable) {
    this.chatService.deleteGoal(message, data);
  }

  onClickSayText(data : Listable) {
    if(data.isClickable) this.chatService.sendMessage(data.text);
  }

}
