export class Message {
  id: string;

  senderId: string;
  senderName: string;

  chatId: string;
  sendDate: Date;
  content: string;
  options?: Array<string>
}
