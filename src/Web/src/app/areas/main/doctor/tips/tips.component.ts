import { Component, OnInit, TemplateRef } from '@angular/core';
import { TableHeaderItem, TableModel, TableItem, Button, TableToolbarActions } from 'carbon-components-angular';
import { Router } from '@angular/router';
import { ApiService } from '@IQuality/core/services/api.service';
import { Tip } from '@IQuality/core/models/tip';
import { TipService } from '@IQuality/core/services/tip.service';




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
  public showClose: boolean = true;





  constructor(private _api: ApiService, private _route: Router, private _tipService: TipService) { }

  public async ngOnInit(): Promise<any> {
    this.loadScreen();

    await this._tipService.getAllTipsOfDoctor().then((response) => {
      this.tips = response;
    });

  }

  private loadScreen() {
    this.model.data = this.tips.map(datapoint => [new TableItem({}), new TableItem({})]);

    this.model.header = [new TableHeaderItem({ data: "" }), new TableHeaderItem({ data: "" })];

    setTimeout(() => {
      this.skeletonStateTable = false;
    }, 1000);

    setTimeout(() => {
      this.model.header = [new TableHeaderItem({ data: "Tip Name" }), new TableHeaderItem({ data: "Description" }), new TableHeaderItem({ data: "Action Type" })];

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
