import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProductAttributeSearchView, ProductAttributeView } from '../_model/productAttribute';
import { CommonSearchView } from '../_model/common-search-view';
import { environment } from '../../environments/environment';
import { ProductSearchView, ProductView } from '../_model/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  public async search(_model: ProductAttributeSearchView) {
    return await this.http.post<CommonSearchView<ProductAttributeView>>(environment.API_URL + 'product/postSearch', _model).toPromise();
  }

  public async create(_model: ProductAttributeView) {
    return await this.http.post(environment.API_URL + 'product/postCreate', _model).toPromise();
  }

  public async update(_model: ProductAttributeView) {
    return await this.http.post(environment.API_URL + 'product/postUpdate', _model).toPromise();
  }

  public async getInfoBrand(_productAttributeId: number) {
    return await this.http.get<ProductAttributeView>(environment.API_URL + 'product/getInfoBrand/'+ _productAttributeId).toPromise();
  }

  public async getInfoDesign(_productAttributeId: number) {
    return await this.http.get<ProductAttributeView>(environment.API_URL + 'product/getInfoDesign/'+ _productAttributeId).toPromise();
  }

  public async getInfoType(_productAttributeId: number) {
    return await this.http.get<ProductAttributeView>(environment.API_URL + 'product/getInfoType/'+ _productAttributeId).toPromise();
  }

  public async getInfoColor(_productAttributeId: number) {
    return await this.http.get<ProductAttributeView>(environment.API_URL + 'product/getInfoColor/'+ _productAttributeId).toPromise();
  }

  public async getInfoSize(_productAttributeId: number) {
    return await this.http.get<ProductAttributeView>(environment.API_URL + 'product/getInfoSize/'+ _productAttributeId).toPromise();
  }
  
  public async searchProduct(_model: ProductSearchView) {
    return await this.http.post<CommonSearchView<ProductView>>(environment.API_URL + 'product/postSearchProduct', _model).toPromise();
  }

  
  public async getInfoProduct(_productId: number) {
    return await this.http.get<ProductView>(environment.API_URL + 'master-product/getInfo/' + _productId).toPromise();
  }

  
  
  // public postInquiryProductByText(_model: ProductSearchView): Observable<ProductView> {
  //   return this.http.post<ProductView[]>(environment.API_URL + 'master-product/postInquiryProductByText', _model)
  //     .pipe(
  //       map((res: any) => {
  //         return res;
  //       })
  //     );
  // }

  // public postInquiryProduct(_model: ProductSearchView){
  //   return this.http.post<ProductView[]>(environment.API_URL + 'master-product/postInquiryProductByText', _model).toPromise();
  // }

  // public async postInquiryByManual(_model: ProductSearchView) {
  //   return await this.http.post<ProductView[]>(environment.API_URL + 'master-product/postInquiryByManual', _model).toPromise();
  // }

  
}
