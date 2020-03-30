export class Message implements MessageInterface{
  senderId: string;
  string: string;

  isOtherUser: boolean;
}
interface MessageInterface {
  senderId: string;
  string: string;
}
