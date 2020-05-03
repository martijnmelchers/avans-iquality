import {Message} from "@IQuality/core/models/messages/message";
import {Suggestion} from "@IQuality/core/models/suggestion";
import {ResponseType} from "@IQuality/core/models/messages/response-type";

export class BotMessage extends Message{
  id: string;

  chatId: string;
  sendDate: Date;
  content: string;

  responseType: ResponseType;

  suggestions: Array<Suggestion>;
}
