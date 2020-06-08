import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShareDataService {

  private messageSource = new BehaviorSubject('');
  currentMessage = this.messageSource.asObservable();

  private confirmData = new BehaviorSubject('');
  selectedSales = this.confirmData.asObservable();

  constructor() { }

  changeMessage(message: any) {
    this.messageSource.next(message)
  }

  confirmSales(sales: any) {
    this.confirmData.next(sales)
  }

}
