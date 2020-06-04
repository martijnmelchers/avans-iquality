import {Component, OnInit, TemplateRef} from '@angular/core';
import {HeaderItem, TableData, TableHeaderItem, TableItem, TableModel} from "carbon-components-angular";
import {GoalService} from "@IQuality/core/services/goal.service";
import {AuthenticationService} from "@IQuality/core/services/authentication.service";
import {UserService} from "@IQuality/core/services/user.service";
import {Router} from "@angular/router";
import {ChatService} from "@IQuality/core/services/chat.service";
import {ChatContext} from "@IQuality/core/models/chat-context";
import {BaseChat} from "@IQuality/core/models/base-chat";


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {

  public tipTitle : string;
  public tipDescription: string;
  public firstTime: boolean;
  public actions: TableModel = new TableModel();
  public goals: TableModel = new TableModel();

  constructor(private goalService: GoalService, private userService: UserService, private authService: AuthenticationService,
              private router: Router, private chatService: ChatService) { }

  async ngOnInit(): Promise<void> {
    //this.loadScreen();
    this.tipTitle = "Weight tip of the day:";
    this.tipDescription = "Losing more weight is fun!";

    this.firstTime = await this.userService.firstTime();

    if(!this.firstTime) {
      this.getGoals();
      this.goals.header = [new TableHeaderItem({data: "Goals"})]
      this.goals.data = [[new TableItem({data: "Hello there how are you doing?"})],
        [new TableItem({data: "I am doing fine! thank you very much."})]];

      this.actions.header = [new TableHeaderItem({data: "Type"}), new TableHeaderItem({data: "Description"})];
      this.actions.data = []
    }
    console.log(this.actions.data.length);
  }

  async startTutorial() {
    let patientchat: BaseChat;
    let chats: Array<ChatContext> = await this.chatService.getChats();
    for(let chat of chats){
      if(chat.chat.type === "PatientChat") {
        patientchat = chat.chat;
        break;
      }
    }
    this.router.navigate(["/chat", patientchat.id]);
  }

  private async getGoals(){
    let userId = this.authService.getNameIdentifier
    let goals = await this.goalService.getGoalsFromUser(userId);
    console.log(goals);
    /*for (let goal in goals){
      this.goals.data.push([new TableItem({data: goal.description })])
    }*/
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
