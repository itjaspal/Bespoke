import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonSearchView } from '../_model/common-search-view';
import { environment } from '../../environments/environment';
import { SalesSearchView, SalesView } from '../_model/sales';

@Injectable({
  providedIn: 'root'
})
export class SalesService {

  constructor(private http: HttpClient) { }

  public async search(_model: SalesSearchView) {
    return await this.http.post<CommonSearchView<SalesView>>(environment.API_URL + 'sales/postSearch', _model).toPromise();
  }
}
