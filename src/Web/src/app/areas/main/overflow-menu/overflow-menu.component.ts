import {Component, Input, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {TableHeaderItem, TableItem, TableModel} from "carbon-components-angular";

@Component({
  selector: 'app-overflow-menu',
  styleUrls: ['./overflow-menu.component.scss'],
  template: `
		<ng-template #overflowMenuItemTemplate>
			<ibm-overflow-menu>
				<ibm-overflow-menu-option>
					First Option
				</ibm-overflow-menu-option>
				<ibm-overflow-menu-option>
					Second Option
				</ibm-overflow-menu-option>
				<ibm-overflow-menu-option>
					Third Option
				</ibm-overflow-menu-option>
			</ibm-overflow-menu>
		</ng-template>

    <ibm-table
      style="display: block; width: 650px;"
      [model]="model"
      [sortable]="sortable"
      [showSelectionColumn]="showSelectionColumn"
      [stickyHeader]="stickyHeader"
      [skeleton]="skeleton"
      [isDataGrid]="isDataGrid"
      (rowClick)="onRowClick($event)"
      [striped]="striped">
    </ibm-table>
	`
})
export class OverflowMenuComponent implements OnInit {

  @Input() model = new TableModel();
  @Input() size = "md";
  @Input() showSelectionColumn = true;
  @Input() striped = true;
  @Input() isDataGrid = false;
  @Input() sortable = true;
  @Input() stickyHeader = false;
  @Input() skeleton = false;

  @ViewChild("overflowMenuItemTemplate")
  protected overflowMenuItemTemplate: TemplateRef<any>;

  ngOnInit() {


    this.model.data = [
      //[new TableItem({ data: "Name 1" }), new TableItem({ data: { id: "1" }, template:  this.overflowMenuItemTemplate})],
    ];
    this.model.header = [
      new TableHeaderItem({ data: "Name" }),
      new TableHeaderItem({ data: "Actions" })
    ];
  }

  onRowClick(index: number) {
    console.log("Row item selected:", index);
  }

}
