import { Injectable } from '@angular/core';
import {ApiService} from "./api.service";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
    constructor(private apiService: ApiService) { }
    createInviteLink(): string {
      return "";
    }

    acceptInviteLink(): void {

    }

    declineInviteLink(): void {

    }
}
