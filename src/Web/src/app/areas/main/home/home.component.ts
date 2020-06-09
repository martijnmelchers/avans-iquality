import {Component, OnInit} from '@angular/core';
import {TableHeaderItem, TableItem, TableModel, TableHead} from "carbon-components-angular";
import {GoalService} from "@IQuality/core/services/goal.service";
import {AuthenticationService} from "@IQuality/core/services/authentication.service";
import {UserService} from "@IQuality/core/services/user.service";
import {Router} from "@angular/router";
import {ChatService} from "@IQuality/core/services/chat.service";
import {ChatContext} from "@IQuality/core/models/chat-context";
import {BaseChat} from "@IQuality/core/models/base-chat";
import {ActionService} from "@IQuality/core/services/action.service";
import { TipService } from '@IQuality/core/services/tip.service';
import { Tip } from '@IQuality/core/models/tip';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {

  public tipTitle: string;
  public tipDescription: string;
  public retrievedTip: any = {};

  public userName: string;
  public userRole: string;

  public firstTime: boolean;
  public actions: any;
  public goals: TableModel = new TableModel();
  public actionTypes: any
  public userInformation = new TableModel();
  public reminderIntervals = []
  public intervalText = "Set Interval"

  constructor(private goalService: GoalService, private actionService: ActionService, private tipService: TipService, private userService: UserService,
              private authService: AuthenticationService, private router: Router, private chatService: ChatService) {
  }

  async ngOnInit(): Promise<void> {
    //this.loadScreen();
    this.tipTitle = "Tip of the day:";
    this.tipDescription = "No tips yet! please come back here when you have some actions!";

    await this.retrieveTipForPatient();

    this.firstTime = await this.userService.firstTime();
    let userId = this.authService.getNameIdentifier;
    this.userRole = this.authService.getRole;
    this.userName = this.authService.getName;

    let goals = await this.getGoals(userId);

    if (this.authService.getRole == 'patient') {
      await this.setActions(userId);
      await this.setActionTypes();
      this.setReminderIntervals();
    }

    this.goals.header = [new TableHeaderItem({data: "Goals"})];

    this.userInformation.header = [new TableHeaderItem({data: "ID"}), new TableHeaderItem({data: "Role"})];
    this.userInformation.data = [[new TableItem({data: userId}), new TableItem({data: this.userRole})]];

    //Moet eerste element meteen meegeven anders geeft de table een rare space in het midden

    this.goals.data = [[new TableItem({data: goals[0]?.description ?? ""})]]
    for (let i = 1; i < goals.length; i++){
      this.goals.data.push([new TableItem({data: goals[i].description})]);
    }

  }

  async startTutorial() {
    let patientChat: BaseChat;
    let chats: Array<ChatContext> = await this.chatService.getChats();
    for (let chat of chats) {
      if (chat.chat.type === "PatientChat") {
        patientChat = chat.chat;
        break;
      }
    }
    this.router.navigate(["/chat", patientChat.id]);
  }

  public getInviteName() {
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

  private async setActions(userId: string){
    this.actions = await this.actionService.getActionsFromUser(userId);
  }

  private async setActionTypes() {
    this.actionTypes = await this.actionService.getActionTypes();
  }

  public getActionTypeFromNumber(actionType: number){
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

  public async retrieveTipForPatient() {
    await this.tipService.getRandomTip().then((response) => {
      if (response.id !== null)
        this.retrievedTip = response;
    });

    if (this.retrievedTip.id == null) {
      this.retrievedTip.name = this.tipTitle;
      this.retrievedTip.description = this.tipDescription;
    }
  }

  public setReminderIntervals() {
    this.reminderIntervals.push("Never")
    this.reminderIntervals.push("Daily")
    this.reminderIntervals.push("Weekly")
    this.reminderIntervals.push("Monthly")
  }

  async setReminderInterval(actionId, index) {
    this.intervalText = this.reminderIntervals[index];
    await this.actionService.setReminderInterval(actionId,index);


  }

}
