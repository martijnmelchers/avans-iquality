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
  data = [
    {
      "group": "Dataset 1",
      "date": "2018-12-31T23:00:00.000Z",
      "value": 10000,
      "surplus": 220447837.99174672
    },
    {
      "group": "Dataset 1",
      "date": "2019-01-04T23:00:00.000Z",
      "value": 65000,
      "surplus": 462977487.2527665
    },
    {
      "group": "Dataset 1",
      "date": "2019-01-07T23:00:00.000Z",
      "value": null,
      "surplus": 22508.505908546234
    },
    {
      "group": "Dataset 1",
      "date": "2019-01-12T23:00:00.000Z",
      "value": 49213,
      "surplus": 1034701440.0405518
    },
    {
      "group": "Dataset 1",
      "date": "2019-01-16T23:00:00.000Z",
      "value": 51213,
      "surplus": 687098199.6510575
    },
    {
      "group": "Dataset 2",
      "date": "2019-01-01T23:00:00.000Z",
      "value": 0,
      "surplus": 21316.19607602561
    },
    {
      "group": "Dataset 2",
      "date": "2019-01-05T23:00:00.000Z",
      "value": 57312,
      "surplus": 185057687.28537452
    },
    {
      "group": "Dataset 2",
      "date": "2019-01-07T23:00:00.000Z",
      "value": 27432,
      "surplus": 310064638.8028874
    },
    {
      "group": "Dataset 2",
      "date": "2019-01-14T23:00:00.000Z",
      "value": 70323,
      "surplus": 1188025078.2585537
    },
    {
      "group": "Dataset 2",
      "date": "2019-01-18T23:00:00.000Z",
      "value": 21300,
      "surplus": 40006577.71128872
    },
    {
      "group": "Dataset 3",
      "date": "2018-12-31T23:00:00.000Z",
      "value": 50000,
      "surplus": 1045282246.6848782
    },
    {
      "group": "Dataset 3",
      "date": "2019-01-04T23:00:00.000Z",
      "value": null,
      "surplus": 22110.82325388951
    },
    {
      "group": "Dataset 3",
      "date": "2019-01-07T23:00:00.000Z",
      "value": 18000,
      "surplus": 49890168.96299
    },
    {
      "group": "Dataset 3",
      "date": "2019-01-12T23:00:00.000Z",
      "value": 39213,
      "surplus": 790578803.5770699
    },
    {
      "group": "Dataset 3",
      "date": "2019-01-16T23:00:00.000Z",
      "value": 61213,
      "surplus": 441227527.281891
    },
    {
      "group": "Dataset 4",
      "date": "2019-01-01T23:00:00.000Z",
      "value": 20000,
      "surplus": 158218672.20708153
    },
    {
      "group": "Dataset 4",
      "date": "2019-01-05T23:00:00.000Z",
      "value": 37312,
      "surplus": 448470409.2778529
    },
    {
      "group": "Dataset 4",
      "date": "2019-01-07T23:00:00.000Z",
      "value": 51432,
      "surplus": 565995382.106869
    },
    {
      "group": "Dataset 4",
      "date": "2019-01-14T23:00:00.000Z",
      "value": 25332,
      "surplus": 516062595.74679893
    },
    {
      "group": "Dataset 4",
      "date": "2019-01-18T23:00:00.000Z",
      "value": null,
      "surplus": 1566.3197279331075
    }
  ];

  options = {
    "title": "Line (time series)",
    "axes": {
      "bottom": {
        "title": "2019 Annual Sales Figures",
        "mapsTo": "date",
        "scaleType": "time"
      },
      "left": {
        "mapsTo": "value",
        "title": "Conversion rate",
        "scaleType": "linear"
      }
    },
    "curve": "curveMonotoneX",
    "height": "400px"
  };

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
