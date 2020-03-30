import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

    constructor() { }

    createInviteLink(): string {
      return "";
    }
}
