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
