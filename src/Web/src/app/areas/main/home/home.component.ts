import { Component, OnInit } from '@angular/core';
import {HeaderItem, TableData, TableHeaderItem, TableItem, TableModel} from "carbon-components-angular";
import {ChatService} from "@IQuality/core/services/chat.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  public tipTitle : string;
  public tipDescription: string;

  public actions: TableModel = new TableModel();
  public goals: TableModel = new TableModel();


  constructor(private chatService: ChatService) { }

  ngOnInit(): void {
    //this.loadScreen();
    this.tipTitle = "Weight tip of the day:";
    this.tipDescription = "Losing more weight is fun!";


    this.goals.header = [new TableHeaderItem({data: "Goals"})]
    this.goals.data = [[new TableItem({data: "Hello there how are you doing?"})],
      [new TableItem({data: "I am doing fine! thank you very much."})]];

    this.actions.header = [new TableHeaderItem({ data: "Type" }), new TableHeaderItem({ data: "Description" })]
    this.actions.data = [[new TableItem({data:"Weight"}), new TableItem({data: "Try and run more!"})]]
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
