import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CatalogTypeSearchView, CatalogTypeView } from '../_model/catalog-type';
import { CommonSearchView } from '../_model/common-search-view';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CatalogTypeService {

  constructor(private http: HttpClient) { }

  public async getTypeCatalog(_id: number) {
    return await this.http.get(environment.API_URL + 'catalog-type/get-type-catalog/'+ _id).toPromise();
  }

  public async getType(_id: number) {
    return await this.http.get(environment.API_URL + 'catalog-type/get-type/'+ _id).toPromise();
  }

  public async getFilterType(_id: number , _type: string) {
    return await this.http.get(environment.API_URL + 'catalog-type/get-filter-type/'+ _id+'/'+_type).toPromise();
  }


  public async search(_model: CatalogTypeSearchView) { 
    return await this.http.post<CommonSearchView<CatalogTypeView>>(environment.API_URL + 'catalog-type/postSearch',_model).toPromise();
  }

  public async create(_model: CatalogTypeView) {
    return await this.http.post<number>(environment.API_URL + 'catalog-type/postCreate', _model).toPromise();
  }

  public async update(_model: CatalogTypeView) {
    return await this.http.post<number>(environment.API_URL + 'catalog-type/postUpdate', _model).toPromise();
  }

  public async getInfo(_id: number) {
    return await this.http.get<CatalogTypeView>(environment.API_URL + 'catalog-type/getInfo/' + _id).toPromise();
  }

  public async delete(params) {
    return await this.http.post(environment.API_URL + 'catalog-type/post/Delete',params).toPromise();
  }
}
