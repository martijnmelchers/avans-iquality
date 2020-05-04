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
  public suggestions = Array<Suggestion>();
  public options: Array<string>;

  constructor(public auth: AuthenticationService, public chatService: ChatService) { }

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

  onClickSayText(text: string) {
    this.chatService.sendMessage(text);
  }
}
