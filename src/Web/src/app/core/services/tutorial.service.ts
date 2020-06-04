import { Injectable } from '@angular/core';
import {BotMessage} from "@IQuality/core/models/messages/bot-message";
import {Suggestion} from "@IQuality/core/models/suggestion";

@Injectable({
  providedIn: 'root'
})
export class TutorialService {

  public currentStage: TutorialStage = TutorialStage.Goal;

  public goalText: string = "Hi there, I noticed that you haven't created any goals yet. A goal is a personal long term milestone. A goal can be almost anything For example I want to be able to visit a festival. Click the Create Goal button below to create your first goal";
  public actionText: string = "Congratulations, you just created your first goal! Let's create some actions. An action is a task that helps you progress towards your goal. For example: Walk for 10 minutes";
  public weightText: string = "Great! You have everything you need to get started. To keep track of your progress, you can provide your weight, bloodpressure and cholesterol. Just ask me to write it down for you.";
  public graphText: string = "Duly noted! You can ask me for a graph of your weight so you can see your progress.";

  constructor() { }

  public getTutorialMessage(): BotMessage{
    let botMessage = new BotMessage();
    switch(this.currentStage){
      case TutorialStage.Goal:
        botMessage.content = this.goalText;
        botMessage.suggestions = [new Suggestion("Create Goal", "I want to set a goal")];
        this.currentStage++;
        break;
      case TutorialStage.Action:
        botMessage.content = this.actionText;
        botMessage.suggestions = [new Suggestion("Create Action", "I want to set a new action")];
        this.currentStage++;
        break;
      case TutorialStage.Weight:
        botMessage.content = this.weightText;
        botMessage.suggestions = [new Suggestion("Note Weight", "Note weight")];
        this.currentStage++;
        break;
      case TutorialStage.Graph:
        botMessage.content = this.graphText;
        botMessage.suggestions = [new Suggestion("Weight Graph", "Weight graph")];
        break;
    }
    return botMessage;
  }
}

enum TutorialStage {
  Goal ,
  Action,
  Weight,
  Graph
}
