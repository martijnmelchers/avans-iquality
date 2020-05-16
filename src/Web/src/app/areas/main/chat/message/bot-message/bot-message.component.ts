import { Component, Input, OnInit } from '@angular/core';
import { Suggestion } from "@IQuality/core/models/suggestion";
import { Listable } from "@IQuality/core/models/listable";
import { ChatService } from "@IQuality/core/services/chat.service";
import { BotMessage } from "@IQuality/core/models/messages/bot-message";
import { Message } from "@IQuality/core/models/messages/message";
import { ResponseType } from "@IQuality/core/models/messages/response-type";

@Component({
  selector: 'app-bot-message',
  templateUrl: './bot-message.component.html',
  styleUrls: ['./bot-message.component.scss']
})
export class BotMessageComponent implements OnInit {

  @Input("message") message: Message

  public botMessage: BotMessage;
  public suggestions = Array<Suggestion>();
  public options: any;


  constructor(public chatService : ChatService) { }

  ngOnInit(): void {
    this.botMessage = this.message as BotMessage

    console.log(this.botMessage);
    if(this.botMessage.responseType == ResponseType.Graph) {
      this.options = {
        "title": this.botMessage.graphData.title,
        "axes": {
        "bottom": {
          "title": this.botMessage.graphData.options.bottom.title,
            "mapsTo": this.botMessage.graphData.options.bottom.mapsTo,
            "scaleType": this.botMessage.graphData.options.bottom.scaleType
        },
        "left": {
          "title": this.botMessage.graphData.options.left.title,
            "mapsTo": this.botMessage.graphData.options.left.mapsTo,
            "scaleType": this.botMessage.graphData.options.left.scaleType
        }
      },
        "curve": "curveMonotoneX",
      };
    }


    this.suggestions.push(new Suggestion("Show active goals", "Get goals"));
    this.suggestions.push(new Suggestion("Set goal", "Set goal"));
  }

  public onSuggestionClicked(suggestion : Suggestion) {
    this.chatService.sendMessage(suggestion.value);
  }

  onClickDelete(message: BotMessage, data: Listable) {
    this.chatService.deleteGoal(message, data);
  }

  onClickSayText(data : Listable) {
    if(data.isClickable) this.chatService.sendMessage(data.text);
  }

}
