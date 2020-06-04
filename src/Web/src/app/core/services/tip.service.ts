import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Tip } from '../models/tip';

@Injectable({
  providedIn: 'root'
})
export class TipService {

  constructor(private _api: ApiService) {

  }

  getAllTipsOfDoctor() {
    return this._api.get<Array<Tip>>('/tip');
  }

  getTipById(id: string) {
    return this._api.get<any>(`/tip/${id}`);
  }

  getRandomTip() {
    return this._api.get<any>('/tip/getrandomtip');
  }

  createTip(data: Tip) {
    this._api.post<Tip>('/tip', data);
  }

  editTip(id: string, data: Tip) {
    this._api.put(`/tip/${id}`, data);
  }

  deleteTip(id: string) {
    this._api.delete(`/tip/${id}`);
  }
}
