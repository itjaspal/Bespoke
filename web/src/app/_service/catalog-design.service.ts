import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CatalogMastSearchView, CatalogMastView } from '../_model/catalog-mast';
import { CommonSearchView } from '../_model/common-search-view';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CatalogDesignService {

  constructor(private http: HttpClient) { }

  public async search(_model: CatalogMastSearchView) { 
    return await this.http.post<CommonSearchView<CatalogMastSearchView>>(environment.API_URL + 'color-font/postSearch',_model).toPromise();
  }

  public async create(_model: CatalogMastView) {
    return await this.http.post<number>(environment.API_URL + 'catalog-mast/postCreate', _model).toPromise();
  }

  public async update(_model: CatalogMastView) {
    return await this.http.post<number>(environment.API_URL + 'catalog-mast/postUpdate', _model).toPromise();
  }

  public async getInfo(_id: number) {
    return await this.http.get<CatalogMastView>(environment.API_URL + 'catalog-mast/getInfo/' + _id).toPromise();
  }

  public async delete(params) {
    return await this.http.post(environment.API_URL + 'catalog-mast/post/Delete',params).toPromise();
  }

}
