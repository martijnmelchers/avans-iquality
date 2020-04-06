import { Component, OnInit } from '@angular/core';
import { TableData, TableHeaderItem, TableItem, TableModel } from "carbon-components-angular";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public skeletonStateTable: boolean = true;
  public model: TableModel = new TableModel();
  private  dataset = [
    { name: "Apple", type: "Fruit" },
    { name: "Grape", type: "Fruit" },
    { name: "Eggplant", type: "Fruit" },
    { name: "Lettuce", type: "Vegetable" },
    { name: "Daikon Radish", type: "Vegetable" },
    { name: "Beef", type: "Meat" }
  ];

  constructor() { }

  ngOnInit(): void {
    this.loadScreen();
  }

  private loadScreen() {
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
  }
}
