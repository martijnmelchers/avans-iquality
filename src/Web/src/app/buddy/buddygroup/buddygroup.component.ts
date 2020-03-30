import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/core/services/api.service';
import { TableData, TableHeaderItem, TableItem, TableModel } from "carbon-components-angular";

@Component({
  selector: 'app-buddygroup',
  templateUrl: './buddygroup.component.html',
  styleUrls: ['./buddygroup.component.scss']
})
export class BuddygroupComponent implements OnInit {
  
  public skeletonStateTable: boolean = true;
  public model: TableModel = new TableModel();
  buddies: any;
  
  constructor(private api: ApiService) {
    
  }

  async ngOnInit(): Promise<void> {
    this.buddies = await this.api.get<string>('/buddygroup/index');
    console.log(this.buddies);
    this.loadScreen();
  }

  private loadScreen() {
    this.model.data = this.buddies.map(datapoint => [new TableItem({}), new TableItem({}), new TableItem({})]);

    this.model.header = [new TableHeaderItem({ data: "" }), new TableHeaderItem({ data: "" }), new TableHeaderItem({ data: "" })];

    setTimeout(() => {
      this.skeletonStateTable = false;
    }, 4000);

    setTimeout(() => {
      this.model.header = [new TableHeaderItem({ data: "Id" }), new TableHeaderItem({ data: "Name" }), new TableHeaderItem({ data: "PhoneNumber" })];

      this.model.data = this.buddies.map(datapoint =>
        [
          new TableItem({ data: datapoint.id }),
          new TableItem({ data: datapoint.name }),
          new TableItem({ data: datapoint.phoneNumber })
        ]
      );
    }, 4000);
  }

  AddBuddy
}




