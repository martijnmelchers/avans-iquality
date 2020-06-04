import { Injectable } from '@angular/core';
import {BotMessage} from "@IQuality/core/models/messages/bot-message";
import {Suggestion} from "@IQuality/core/models/suggestion";
import {ResponseType} from "@IQuality/core/models/messages/response-type";
import {ApiService} from "@IQuality/core/services/api.service";
import {UserService} from "@IQuality/core/services/user.service";
import {AuthenticationService} from "@IQuality/core/services/authentication.service";

@Injectable({
  providedIn: 'root'
})
export class TutorialService {

  public currentStage: TutorialStage = TutorialStage.Goal;

  public goalText: string = "Hi there, I noticed that you haven't created any goals yet. A goal is a personal long term milestone. A goal can be almost anything For example I want to be able to visit a festival. Click the Create Goal button below to create your first goal";
  public actionText: string = "Congratulations, you just created your first goal! Let's create some actions. An action is a task that helps you progress towards your goal. For example: Walk for 10 minutes";
  public weightText: string = "Great! You have everything you need to get started. To keep track of your progress, you can provide your weight, bloodpressure and cholesterol. Just ask me to write it down for you.";
  public graphText: string = "Duly noted! You can ask me for a graph of your weight so you can see your progress.";

  constructor(private api: ApiService, private auth: AuthenticationService) { }

  public getTutorialMessage(chatId: string): any{
    let botMessage = new BotMessage();
    botMessage.chatId = chatId;
    botMessage.type = "BotMessage";
    switch(this.currentStage){
      case TutorialStage.Goal:
        botMessage.content = this.goalText;
        botMessage.suggestions = [new Suggestion("Create Goal", "I want to set a goal")];
        break;
      case TutorialStage.Action:
        botMessage.content = this.actionText;
        botMessage.suggestions = [new Suggestion("Create Action", "I want to set a new action")];
        break;
      case TutorialStage.Weight:
        botMessage.content = this.weightText;
        botMessage.suggestions = [new Suggestion("Note Weight", "Note weight")];
        break;
      case TutorialStage.Graph:
        botMessage.content = this.graphText;
        botMessage.suggestions = [new Suggestion("Weight Graph", "Weight graph")];
        this.api.put(`/users/${this.auth.getNameIdentifier}/tutorial`, {});
        break;
    }
    return {content: botMessage.content, suggestions: botMessage.suggestions,
      chatId: chatId, listData: [], type: "BotMessage"};
  }

  public nextStage(){
    this.currentStage++;
  }
}

enum TutorialStage {
  Goal ,
  Action,
  Weight,
  Graph
}
