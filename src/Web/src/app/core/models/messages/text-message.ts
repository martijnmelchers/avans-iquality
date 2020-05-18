import {Message} from "@IQuality/core/models/messages/message";

export class TextMessage extends Message{
  senderName: string;
  content: string;
}
