import {Component, OnInit} from '@angular/core';
import {TableHeaderItem, TableItem, TableModel} from "carbon-components-angular";
import {GoalService} from "@IQuality/core/services/goal.service";
import {AuthenticationService} from "@IQuality/core/services/authentication.service";
import {UserService} from "@IQuality/core/services/user.service";
import {Router} from "@angular/router";
import {ChatService} from "@IQuality/core/services/chat.service";
import {ChatContext} from "@IQuality/core/models/chat-context";
import {BaseChat} from "@IQuality/core/models/base-chat";
import {ActionService} from "@IQuality/core/services/action.service";


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {

  public tipTitle : string;
  public tipDescription: string;

  public userName : string;
  public userRole: string;

  public firstTime: boolean;
  public actions: TableModel = new TableModel();
  public goals: TableModel = new TableModel();
  public userInformation = new TableModel();

  constructor(private goalService: GoalService, private actionService: ActionService, private userService: UserService,
              private authService: AuthenticationService, private router: Router, private chatService: ChatService) { }

  async ngOnInit(): Promise<void> {
    //this.loadScreen();
    this.tipTitle = "Weight tip of the day:";
    this.tipDescription = "Losing more weight is fun!";

    this.firstTime = await this.userService.firstTime();
    let userId = this.authService.getNameIdentifier;
    this.userRole = this.authService.getRole;
    this.userName = this.authService.getName;

    if (!this.firstTime) {
      let goals = await this.getGoals(userId);
      let actions = await this.getActions(userId);

      this.goals.header = [new TableHeaderItem({data: "Goals"})];

      this.userInformation.header = [new TableHeaderItem({data: "ID"}), new TableHeaderItem({data: "Role"})];
      this.userInformation.data = [[new TableItem({data: userId}), new TableItem({data: this.userRole})]];

      //Moet eerste element meteen meegeven anders geeft de table een rare space in het midden
      this.goals.data = [[new TableItem({data: goals[0].description ?? ""})]]
      for (let i = 1; i < goals.length; i++) {
        this.goals.data.push([new TableItem({data: goals[i].description})]);
      }
      this.actions.header = [new TableHeaderItem({data: "Type"}), new TableHeaderItem({data: "Description"})];

      this.actions.data = [[new TableItem({data: HomeComponent.getActionTypeFromNumber(actions[0].type ?? 0)}), new TableItem({data: actions[0].description ?? ""})]]
      for (let i = 1; i < actions.length; i++) {

        let actionType = HomeComponent.getActionTypeFromNumber(actions[i].type);
        this.actions.data.push([new TableItem({data: actionType.toString()}), new TableItem({data: actions[i].description})]);
      }
    }
  }

  async startTutorial() {
    let patientChat: BaseChat;
    let chats: Array<ChatContext> = await this.chatService.getChats();
    for(let chat of chats){
      if(chat.chat.type === "PatientChat") {
        patientChat = chat.chat;
        break;
      }
    }
    this.router.navigate(["/chat", patientChat.id]);
  }
  public getInviteName(){
    switch (this.userRole) {
      case 'doctor':
        return 'patient'
      case 'admin':
        return 'doctor'
      case 'patient':
        return 'buddy'
    }
  }

  private async getGoals(userId: string) {
    return await this.goalService.getGoalsFromUser(userId);
  }

  private async getActions(userId: string){
    return await this.actionService.getActionsFromUser(userId);
  }

  private static getActionTypeFromNumber(actionType: number){
    switch (actionType) {
      case 0:
        return "Weight"
      case 1:
        return "BloodSugar"
      case 2:
        return "BloodPressure"
      case 3:
        return "Cholesterol"
      case 4:
        return "General"
    }
  }

  /*private loadScreen() {
    this.model.data = this.dataset.map(datapoint => [new TableItem({}), new TableItem({})]);

    this.model.header = [new TableHeaderItem({ data: "" }), new TableHeaderItem({ data: "" })];

    setTimeout(() => {
      this.skeletonStateTable = false;
    }, 4000);

    setTimeout(() => {
      this.model.header = [new TableHeaderItem({ data: "Name" }), new TableHeaderItem({ data: "Description" })];

      this.model.data = this.dataset.map(datapoint =>
        [
          new TableItem({ data: datapoint.name }),
          new TableItem({ data: "Lorem ipsum dolor sit amet, consectetur adipiscing elit." })
        ]
      );
    }, 4000);
  }*/

}
