﻿import {Message} from "@IQuality/core/models/messages/message";
import {BaseChat} from "@IQuality/core/models/base-chat";

export class ChatContext {
  chat: BaseChat;
  messages: Array<Message>;
}