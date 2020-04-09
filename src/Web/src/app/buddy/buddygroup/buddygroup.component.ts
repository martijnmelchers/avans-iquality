import { Component, OnInit } from '@angular/core';
import { ApiService } from '@IQuality/core/services/api.service';
import { TableData, TableHeaderItem, TableItem, TableModel, Button } from "carbon-components-angular";

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
    this.buddies = await this.api.get<string>('/buddy');
  }


  async deleteBuddy(id){
    await this.api.delete<string>(`/buddy/${id}`);
    this.ngOnInit();
  }

}




