import {Message} from "@IQuality/core/models/messages/message";
import {Listable} from "@IQuality/core/models/listable";

export class TextMessage extends Message{
  senderName: string;
  text: string;
  roomId: string;
  content: string;

  listData?: Array<Listable>
}
