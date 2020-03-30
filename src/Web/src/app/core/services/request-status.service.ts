import { EventEmitter, Injectable } from '@angular/core';

/**
 * This service shows/hides the progress bar
 *
 * @class RequestStatusService
 */
@Injectable({
  providedIn: 'root'
})
export class RequestStatusService {
  /** The current number of active requests */
  private activeRequests: number = 0;

  public event: EventEmitter<number>;

  /**
   * @ignore
   */
  constructor() {
    this.event = new EventEmitter();
  }

  public get busy(): boolean {
    console.log(this.activeRequests > 0);
    return this.activeRequests > 0;
  }

  /**
   * Start a new request
   */
  public requestStart() {
    this.activeRequests++;
    this.event.emit(this.activeRequests);
  }

  /**
   * Finish a request
   */
  public requestFinish() {
    this.activeRequests--;
    this.event.emit(this.activeRequests);
  }
}
