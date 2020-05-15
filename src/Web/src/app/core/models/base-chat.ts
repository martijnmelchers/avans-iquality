import {Message} from "@IQuality/core/models/messages/message";

export class BaseChat {
  id: string;
  name: string;
  creationDate: Date;

  initiatorId: string;
  messages: Array<Message>;

  // Need this to know if the chat is of buddychat :)
  ay: string;
}
