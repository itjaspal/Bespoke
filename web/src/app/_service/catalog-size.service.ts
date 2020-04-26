import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonSearchView } from '../_model/common-search-view';
import { environment } from '../../environments/environment';
import { CatalogSizeSearchView, CatalogSizeView } from '../_model/catalog-size';

@Injectable({
  providedIn: 'root'
})
export class CatalogSizeService {

  constructor(private http: HttpClient) { }

  public async search(_model: CatalogSizeSearchView) { 
    return await this.http.post<CommonSearchView<CatalogSizeView>>(environment.API_URL + 'catalog-size/postSearch',_model).toPromise();
  } 

  public async create(_model: CatalogSizeView) {
    return await this.http.post<number>(environment.API_URL + 'catalog-size/postCreate', _model).toPromise();
  }

  public async update(_model: CatalogSizeView) {
    return await this.http.post<number>(environment.API_URL + 'catalog-size/postUpdate', _model).toPromise();
  }

  public async getInfo(_id: number) {
    return await this.http.get<CatalogSizeView>(environment.API_URL + 'catalog-size/getInfo/' + _id).toPromise();
  }

  public async delete(params) {
    return await this.http.post(environment.API_URL + 'catalog-size/post/Delete',params).toPromise();
  }
}
