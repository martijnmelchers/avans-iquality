import { Injectable } from '@angular/core';
import { ApiService } from "@IQuality/core/services/api.service";
import { CookieService } from "ngx-cookie-service";
import { JwtHelperService } from "@auth0/angular-jwt";
import {Invite} from "@IQuality/core/models/invite";
import {ChatService} from "@IQuality/core/services/chat.service";
import {ChatContext} from "@IQuality/core/models/chat-context";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
z
  public encodedToken: string;
  public decodedToken: any;
  private tokenService: JwtHelperService;
  private chatService: ChatService;
  private readonly NAME_IDENTIFIER = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
  private readonly NAME = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

  constructor(private readonly _api: ApiService, private _cookie: CookieService) {
    this.tokenService = new JwtHelperService();
    this.encodedToken = this._cookie.get('token');
    this.decodedToken = this.tokenService.decodeToken(this.encodedToken);
  }

  public get loggedIn(): boolean {
    return this.encodedToken && this.tokenService.isTokenExpired(this.encodedToken);
  }

  public get getNameIdentifier(): string {
    if (!this.decodedToken) {
      this.decodedToken = this.tokenService.decodeToken(this.encodedToken);
    }

    return this.decodedToken[this.NAME_IDENTIFIER];
  }

  public get getName(): string {
    if (!this.decodedToken) {
      this.decodedToken = this.tokenService.decodeToken(this.encodedToken);
    }

    return this.decodedToken[this.NAME];
  }

  public get getRole(): string {
    return this.decodedToken.role;
  }

  public saveToken(token: string) {
    this._cookie.set('token', token);
    this.encodedToken = token;
    this.decodedToken = this.tokenService.decodeToken(token);

    if(!this.chatService)
      return;

    this.chatService.disconnect();
  }

  async getInviteLink(inviteToken: string): Promise<Invite> {
    return this._api.get<Invite>(`/invite/${inviteToken}`);
  }

  async createInviteLink(chatId:string = null, email:string): Promise<Invite> {
    let body: object = {ChatId: chatId, Email: email};

    return this._api.post<Invite>('/invite', body);
  }

  public set SetChatService(chatService: ChatService) {
    this.chatService = chatService;
  }
}
