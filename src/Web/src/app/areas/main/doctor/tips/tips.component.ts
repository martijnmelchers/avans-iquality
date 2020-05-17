import { Component, OnInit, TemplateRef } from '@angular/core';
import { TableHeaderItem, TableModel, TableItem, Button, TableToolbarActions } from 'carbon-components-angular';
import { Router } from '@angular/router';
import { ApiService } from '@IQuality/core/services/api.service';
import { Tip } from '@IQuality/core/models/tip';




@Component({
  selector: 'app-tips',
  templateUrl: './tips.component.html',
  styleUrls: ['./tips.component.scss']
})
export class TipsComponent implements OnInit {

  public skeletonStateTable: boolean = true;
  public model: TableModel = new TableModel();
  private tips = [];
  customTableItemTemplate: TemplateRef<any>;




  constructor(private _api: ApiService, private _route: Router) { }

  public async ngOnInit(): Promise<any> {
    this.loadScreen();
    await this._api.get<Array<Tip>>('/tip').then(resp => {
      this.tips = resp;
    });

    console.log(this.tips);

    await this._api.get<Tip>('/tip/getrandomtip').then(resp => {
      console.log('random tip: ', resp);
    })
  }

  private loadScreen() {
    this.model.data = this.tips.map(datapoint => [new TableItem({}), new TableItem({})]);

    this.model.header = [new TableHeaderItem({ data: "" }), new TableHeaderItem({ data: "" })];

    setTimeout(() => {
      this.skeletonStateTable = false;
    }, 1000);

    setTimeout(() => {
      this.model.header = [new TableHeaderItem({ data: "Tip naam" }), new TableHeaderItem({ data: "Beschrijving" }), new TableHeaderItem({ data: "Actie Type" })];

      this.model.data = this.tips.map(datapoint =>
        [
          new TableItem({ data: datapoint.name }),
          new TableItem({ data: datapoint.description }),
          new TableItem({ data: datapoint.actionType }),
        ]
      );


    }, 2000);
  }

  navigateToAdd() {
    this._route.navigateByUrl('/doctor/tips/add');
  }

  manageTip(event) {
    let tip = this.tips[event];

    this._route.navigateByUrl(`/doctor/tips/manage/${tip.id}`);
  }
}
