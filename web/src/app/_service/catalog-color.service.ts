import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CatalogColorSearchView, CatalogColorView } from '../_model/catalog-color';
import { CommonSearchView } from '../_model/common-search-view';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CatalogColorService {

  constructor(private http: HttpClient) { }

  public async search(_model: CatalogColorSearchView) { 
    return await this.http.post<CommonSearchView<CatalogColorView>>(environment.API_URL + 'catalog-color/postSearch',_model).toPromise();
  }

  public async create(_model: CatalogColorView) {
    return await this.http.post<number>(environment.API_URL + 'catalog-color/postCreate', _model).toPromise();
  }

  public async update(_model: CatalogColorView) {
    return await this.http.post<number>(environment.API_URL + 'catalog-color/postUpdate', _model).toPromise();
  }

  public async getInfo(_id: number) {
    return await this.http.get<CatalogColorView>(environment.API_URL + 'catalog-color/getInfo/' + _id).toPromise();
  }

  public async delete(params) {
    return await this.http.post(environment.API_URL + 'catalog-color/post/Delete',params).toPromise();
  }
}
