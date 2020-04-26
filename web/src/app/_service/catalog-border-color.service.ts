import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonSearchView } from '../_model/common-search-view';
import { environment } from '../../environments/environment';
import { CatalogBorderColorSearchView, CatalogBorderColorView } from '../_model/catalog-border-color';

@Injectable({
  providedIn: 'root'
})
export class CatalogBorderColorService {

  constructor(private http: HttpClient) { }

  public async search(_model: CatalogBorderColorSearchView) { 
    return await this.http.post<CommonSearchView<CatalogBorderColorView>>(environment.API_URL + 'catalog-bordercolor/postSearch',_model).toPromise();
  }

  public async create(_model: CatalogBorderColorView) {
    return await this.http.post<number>(environment.API_URL + 'catalog-bordercolor/postCreate', _model).toPromise();
  }

  public async update(_model: CatalogBorderColorView) {
    return await this.http.post<number>(environment.API_URL + 'catalog-bordercolor/postUpdate', _model).toPromise();
  }

  public async getInfo(_id: number) {
    return await this.http.get<CatalogBorderColorView>(environment.API_URL + 'catalog-bordercolor/getInfo/' + _id).toPromise();
  }

  public async delete(params) {
    return await this.http.post(environment.API_URL + 'catalog-bordercolor/post/Delete',params).toPromise();
  }
}
