import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonSearchView } from '../_model/common-search-view';
import { environment } from '../../environments/environment';
import { SalesSearchView, SalesView } from '../_model/sales';
import { CatalogMastSearchView, CatalogMastView } from '../_model/catalog-mast';

@Injectable({
  providedIn: 'root'
})
export class SalesService {

  constructor(private http: HttpClient) { }

  public async search(_model: SalesSearchView) {
    return await this.http.post<CommonSearchView<SalesView>>(environment.API_URL + 'sales/postSearch', _model).toPromise();
  }

  public async searchDesign(_model: CatalogMastSearchView) {
    return await this.http.post<CommonSearchView<CatalogMastView>>(environment.API_URL + 'sales/postSearchDesign', _model).toPromise();
  }

  public async getTypeInCatalogColor(_catalog_id: number , _catalog_color_id: number) {
    return await this.http.get(environment.API_URL + 'sales/get-type-catalog/'+ _catalog_id+'/'+_catalog_color_id).toPromise();
  }

  public async getColorInCatalog(_catalog_id: number) {
    return await this.http.get(environment.API_URL + 'sales/get-color-catalog/'+ _catalog_id).toPromise();
  }

  public async getColorFont(_catalog_id: number) {
    return await this.http.get(environment.API_URL + 'sales/get-color-font/'+ _catalog_id).toPromise();
  }

  public async getEmbroidery() {
    return await this.http.get(environment.API_URL + 'sales/get-embroidery').toPromise();
  }

}
