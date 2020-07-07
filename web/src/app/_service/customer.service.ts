import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { CustomerView, CustomerSearchView } from '../_model/customer-view';
import { CommonSearchView } from '../_model/common-search-view';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpClient) { }

  public postInquiryCustomerByText(_type: string, _txt: string): Observable<CustomerView> {
    return this.http.post<CustomerView[]>(environment.API_URL + 'master-customer/postInquiryCustomerByText', {
      type:_type,
      txt: _txt
    })
      .pipe(
        map((res: any) => {
          return res;
        })
      );
  }

  public async search(_model: CustomerSearchView) { 
    return await this.http.post<CommonSearchView<CustomerView>>(environment.API_URL + 'master-customer/postSearch',_model).toPromise();
  }

  public async create(_model: CustomerView) {
    return await this.http.post<number>(environment.API_URL + 'master-customer/postCreate', _model).toPromise();
  }

  public async delete(params) {
    return await this.http.post(environment.API_URL + 'master-customer/post/Delete',params).toPromise();
  }

  public async getInfo(_id: number) {
    return await this.http.get<CustomerView>(environment.API_URL + 'master-customer/getInfo/' + _id).toPromise();
  }

  public async getInfoByName(_name: string) {
    return await this.http.get<CustomerView>(environment.API_URL + 'master-customer/getInfoByName/' + _name).toPromise();
  }

  public async update(_model: CustomerView) {
    return await this.http.post<number>(environment.API_URL + 'master-customer/postUpdate', _model).toPromise();
  }
}