export class RequestOptions {

  /** Whether or not to display the request loader */
  public disableRequestLoader?: boolean = false;
  /** Whether or not to fetch a local asset */
  public removeEndpoint?: boolean = false;

  /** Headers that are sent with the request */
  public headers?: { [name: string]: string | Array<string> } = {};

  /** The response type you expect back. Must either be: json, text, blob, arrayblob */
  public responseType?: any = 'json';
}
