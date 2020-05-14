import { Component, OnInit } from '@angular/core';
import { TableHeaderItem, TableModel, TableItem } from 'carbon-components-angular';
import { Router } from '@angular/router';




@Component({
  selector: 'app-tips',
  templateUrl: './tips.component.html',
  styleUrls: ['./tips.component.scss']
})
export class TipsComponent implements OnInit {

  public skeletonStateTable: boolean = true;
  public model: TableModel = new TableModel();
  private  dataset = [
    { name: "Vermijd snoep", type: "Bloodsugar" },
    { name: "Vermijd drank", type: "Cholesterol" },
    { name: "Wissel van positie", type: "General" },
    { name: "Drink water", type: "General" },
    { name: "Ga naar buiten", type: "Weight" },
    { name: "Slaap op tijd", type: "Blood Pressure" },
    { name: "Val af", type: "Weight" }
  ];

 

  constructor(private route: Router) { }

  ngOnInit(): void {
    this.loadScreen();
  }

  private loadScreen() {
    this.model.data = this.dataset.map(datapoint => [new TableItem({}), new TableItem({})]);

    this.model.header = [new TableHeaderItem({ data: "" }), new TableHeaderItem({ data: "" })];

    setTimeout(() => {
      this.skeletonStateTable = false;
    }, 1000);

    setTimeout(() => {
      this.model.header = [new TableHeaderItem({ data: "Tip naam" }), new TableHeaderItem({ data: "Beschrijving" }), new TableHeaderItem({ data: "Actie Type" })];

      this.model.data = this.dataset.map(datapoint =>
        [
          new TableItem({ data: datapoint.name }),
          new TableItem({ data: "Lorem ipsum"}),
          new TableItem({ data: datapoint.type})
        ]
      );
    }, 4000);
  }

  navigateToAdd(){
    this.route.navigateByUrl('/doctor/tips/add');
  }
}
