import {Message} from "@IQuality/core/models/messages/message";
import {Suggestion} from "@IQuality/core/models/suggestion";
import {ResponseType} from "@IQuality/core/models/messages/response-type";
import {Listable} from "@IQuality/core/models/listable";
import {GraphData} from "@IQuality/core/models/graph-data/graph-data";

export class BotMessage extends Message{
  id: string;

  chatId: string;
  sendDate: Date;
  content: string;

  listData?: Array<Listable>
  graphData? : GraphData

  responseType: ResponseType;

  suggestions: Array<Suggestion>;
}
