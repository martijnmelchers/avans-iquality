export class BaseChat {
  id: string;
  name: string;
  creationDate: Date;

  initiatorId: string;
  messages: Array<string>;
}
