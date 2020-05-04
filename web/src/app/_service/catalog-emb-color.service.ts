import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonSearchView } from '../_model/common-search-view';
import { environment } from '../../environments/environment';
import { CatalogEmbColorSearchView, CatalogEmbColorView } from '../_model/catalog-emb-color';

@Injectable({
  providedIn: 'root'
})
export class CatalogEmbColorService {

  constructor(private http: HttpClient) { }

  public async getColor(_id: number) {
    //return await this.http.get(environment.API_URL + 'master-user-role/function-group/get/'+isPC).toPromise();
    return await this.http.get(environment.API_URL + 'catalog-embcolor/get-color/'+ _id).toPromise();
  }

  public async search(_model: CatalogEmbColorSearchView) { 
    return await this.http.post<CommonSearchView<CatalogEmbColorView>>(environment.API_URL + 'catalog-embcolor/postSearch',_model).toPromise();
  }

  public async create(_model: CatalogEmbColorView) {
    return await this.http.post<number>(environment.API_URL + 'catalog-embcolor/postCreate', _model).toPromise();
  }

  public async update(_model: CatalogEmbColorView) {
    return await this.http.post<number>(environment.API_URL + 'catalog-embcolor/postUpdate', _model).toPromise();
  }

  public async getInfo(_id: number) {
    return await this.http.get<CatalogEmbColorView>(environment.API_URL + 'catalog-embcolor/getInfo/' + _id).toPromise();
  }

  public async delete(params) {
    return await this.http.post(environment.API_URL + 'catalog-embcolor/post/Delete',params).toPromise();
  }

  public async updateEmbColor(model) {
    return await this.http.post(environment.API_URL + 'catalog-embcolor/postUpdateEmbColor', model).toPromise();
  }
}
