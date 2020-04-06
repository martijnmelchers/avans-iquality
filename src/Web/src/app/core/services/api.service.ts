import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { finalize } from 'rxjs/operators';
import {environment} from "../../../environments/environment";
import {RequestOptions} from "../models/request-options";
import {RequestStatusService} from "./request-status.service";

/**
 * This service handles all API requests to external API's
 *
 * @class ApiService
 */
@Injectable({
  providedIn: 'root'
})
export class ApiService {

  /**
   * @ignore
   */
  constructor(private readonly _http: HttpClient,
              private readonly _requestService: RequestStatusService) {
    this._endpoint = environment.endpoints.api;
  }

  /** The endpoint for the API */
  private _endpoint: string;

  /**
   * Get the current endpoint
   *
   * @return {string}
   */
  get endpoint(): string {
    return this._endpoint;
  }

  /**
   * Set the endpoint
   *
   * @param {string} url
   */
  set endpoint(url: string) {
    this._endpoint = url;
  }


  /**
   * Send a new GET request to an external API
   *
   * @param {string} url
   * @param parameters
   * @param {RequestOptions} requestOptions
   * @return {Promise<T>}
   */
  public async get<T>(url: string, parameters: any = null, requestOptions: RequestOptions = {}): Promise<T> {
    if (!requestOptions.disableRequestLoader) this._requestService.requestStart();

    if (requestOptions.disableAuthentication) requestOptions.headers['disableAuthentication'] = 'true';

    return await this._http.get<T>(`${requestOptions.removeEndpoint ? '' : this.endpoint}${url}`, {
      responseType: requestOptions.responseType,
      params: parameters,
      headers: new HttpHeaders(requestOptions.headers)
    }).pipe(finalize(() => !requestOptions.disableRequestLoader && this._requestService.requestFinish())).toPromise<T>();
  }

  /**
   * Send a new POST request to an external API
   *
   * @param {string} url
   * @param {object} body
   * @param parameters
   * @param {RequestOptions} requestOptions
   * @return {Promise<T>}
   */
  public async post<T>(url: string, body: object | string, parameters: any = null, requestOptions: RequestOptions = {}): Promise<T> {
    if (!requestOptions.disableRequestLoader) this._requestService.requestStart();

    if (requestOptions.disableAuthentication) requestOptions.headers['disableAuthentication'] = 'true';

    return await this._http.post<T>(`${requestOptions.removeEndpoint ? '' : this.endpoint}${url}`, body, {
      responseType: requestOptions.responseType,
      params: parameters,
      headers: new HttpHeaders(requestOptions.headers)
    }).pipe(finalize(() => !requestOptions.disableRequestLoader && this._requestService.requestFinish())).toPromise<T>();
  }

  /**
   * Send a new PUT request to an external API
   *
   * @param {string} url
   * @param body
   * @param parameters
   * @param {RequestOptions} requestOptions
   * @return {Promise<T>}
   */
  public async put<T>(url: string, body: object | string, parameters: any = null, requestOptions: RequestOptions = {}): Promise<T> {
    if (!requestOptions.disableRequestLoader) this._requestService.requestStart();

    if (requestOptions.disableAuthentication) requestOptions.headers['disableAuthentication'] = 'true';

    return await this._http.put<T>(`${requestOptions.removeEndpoint ? '' : this.endpoint}${url}`, body, {
      responseType: requestOptions.responseType,
      params: parameters,
      headers: new HttpHeaders(requestOptions.headers)
    }).pipe(finalize(() => !requestOptions.disableRequestLoader && this._requestService.requestFinish())).toPromise<T>();
  }

  /**
   * Send a new PATCH request to an external API
   *
   * @param {string} url
   * @param body
   * @param parameters
   * @param {RequestOptions} requestOptions
   * @return {Promise<T>}
   */
  public async patch<T>(url: string, body: object | string, parameters: any = null, requestOptions: RequestOptions = {}): Promise<T> {
    if (!requestOptions.disableRequestLoader) this._requestService.requestStart();

    if (requestOptions.disableAuthentication) requestOptions.headers['disableAuthentication'] = 'true';

    return await this._http.patch<T>(`${requestOptions.removeEndpoint ? '' : this.endpoint}${url}`, body, {
      responseType: requestOptions.responseType,
      params: parameters,
      headers: new HttpHeaders(requestOptions.headers)
    }).pipe(finalize(() => !requestOptions.disableRequestLoader && this._requestService.requestFinish())).toPromise<T>();
  }

  /**
   * Send a new DELETE request to an external API
   *
   * @param {string} url
   * @param parameters
   * @param {RequestOptions} requestOptions
   * @return {Promise<T>}
   */
  public async delete<T>(url: string, parameters: any = null, requestOptions: RequestOptions = {}): Promise<T> {
    if (!requestOptions.disableRequestLoader) this._requestService.requestStart();

    if (requestOptions.disableAuthentication) requestOptions.headers['disableAuthentication'] = 'true';

    return await this._http.delete<T>(`${requestOptions.removeEndpoint ? '' : this.endpoint}${url}`, {
      responseType: requestOptions.responseType,
      params: parameters,
      headers: new HttpHeaders(requestOptions.headers)
    }).pipe(finalize(() => !requestOptions.disableRequestLoader && this._requestService.requestFinish())).toPromise<T>();
  }
}
