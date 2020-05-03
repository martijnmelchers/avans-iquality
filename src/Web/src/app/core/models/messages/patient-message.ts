import {Message} from "@IQuality/core/models/messages/message";

export class PatientMessage extends Message{
  senderName: string;
  text: string;
  roomId: string;
  content: string;

  listData?: Array<string>
}
