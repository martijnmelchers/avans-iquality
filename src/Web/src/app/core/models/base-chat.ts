import {Message} from "@IQuality/core/models/messages/message";

export class BaseChat {
  id: string;
  name: string;
  creationDate: Date;

  initiatorId: string;
  messages: Array<Message>;

  type: string;
}
