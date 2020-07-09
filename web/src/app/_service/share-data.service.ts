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

  private oldData = new BehaviorSubject('');
  oldSales = this.oldData.asObservable();

  constructor() { }

  changeMessage(message: any) {
    this.messageSource.next(message)
  }

  confirmSales(sales: any) {
    this.confirmData.next(sales)
  }

  editSales(old: any) {
    this.oldData.next(old)
  }

}
