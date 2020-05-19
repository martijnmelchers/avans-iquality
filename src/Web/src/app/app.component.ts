import { Component } from '@angular/core';
import { NgProgress, NgProgressRef } from "ngx-progressbar";
import { RequestStatusService } from "./core/services/request-status.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {

  public active: boolean;
  public hasHamburger: boolean;

  constructor(
    progress: NgProgress,
    requestStatus: RequestStatusService
  ) {
    requestStatus.event.subscribe(activeRequests => {
      const progressRef: NgProgressRef = progress.ref();
      
      if (activeRequests === 1)
        progressRef.start();

      if (activeRequests === 0)
        progressRef.complete();
    });

    this.hasHamburger = window.innerWidth < 1055;
    requestStatus.requestStart();
    setTimeout(() => requestStatus.requestFinish(), 2500);
  }

  onResize($event: any) {
    this.hasHamburger = window.innerWidth < 1055;
  }
}
