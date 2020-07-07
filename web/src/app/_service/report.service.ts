import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { saveAs } from 'file-saver';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(private http: HttpClient) { }

    public async dailySalesReport(_model: any) {
        return await this.http.post(environment.API_URL + 'report/postSaleTransactionReport', _model).toPromise();
    }

    public async dailySalesDetailReport(_model: any) {
      return await this.http.post(environment.API_URL + 'report/postSaleTransactionDetailReport', _model).toPromise();
  }
}
