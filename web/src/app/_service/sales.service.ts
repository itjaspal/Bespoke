import { SalesAttachView, SalesTransactionUpdateStatusView } from './../_model/sales';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonSearchView } from '../_model/common-search-view';
import { environment } from '../../environments/environment';
import { SalesSearchView, SalesView, DocNoView, DocNoSearchView, SalesTransactionView } from '../_model/sales';
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

  // public async getDocNo(_branch_id: number) {
  //   return await this.http.get(environment.API_URL + 'sales/get-docNo/'+ _branch_id).toPromise();
  // }
  public async searchDocNo(_model: DocNoSearchView) {
       return await this.http.post<DocNoView>(environment.API_URL + 'sales/postSearchDocNo', _model).toPromise();
  }
  
  public async create(_model: SalesTransactionView) {
    return await this.http.post<number>(environment.API_URL + 'sales/postCreate', _model).toPromise();
  }

  public async update(_model: SalesTransactionView) {
    return await this.http.post<number>(environment.API_URL + 'sales/postUpdate', _model).toPromise();
  }
  
  public async postCancelSalesTransaction(_model: any) {
    return await this.http.post<number>(environment.API_URL + 'sales/postCancelSalesTransaction', _model).toPromise();
  }

  public async getInquirySalesTransactionInfo(_saleTransactionId : number) {
    return await this.http.get<SalesTransactionView>(environment.API_URL + 'sales/getInquirySalesTransactionInfo/' + _saleTransactionId).toPromise();
  }

  public async postSalesAttach(_model: SalesAttachView) {
    var fd = new FormData();
    fd.append('file', _model.file);
    fd.append('pic_base64',_model.pic_base64);
    fd.append('co_trns_mast_id', _model.co_trns_mast_id.toString());
    fd.append('pic_file_path',_model.pic_file_path);
    //fd.append('pic_base64',_model.pic_base64);
    
    
    return await this.http.post<number>(environment.API_URL + 'sales/postSalesAttach', fd).toPromise();
    
  }

  public async getInquiryAttachFile(_saleTransactionId: number) {
    //return this.http.post<SalesAttachView[]>(environment.API_URL + 'sales/postInquiryAttachFile/', _saleTransactionId).toPromise();
    return await this.http.get(environment.API_URL + 'sales/getInquiryAttachFile/'+ _saleTransactionId).toPromise();
  }

  public async deleteAttachFile(params) {
    return await this.http.post(environment.API_URL + 'sales/postDeleteAttachFile',params).toPromise();
  }

  

  public async postUpdateToReady(_model: any) {
    return await this.http.post<number>(environment.API_URL + 'sales/postUpdateToReady', _model).toPromise();
  }

  public async getSalesTransactionInfo(_saleTransactionId) {
    return await this.http.get<SalesTransactionView>(environment.API_URL + 'sales/getSalesTransactionInfo/' + _saleTransactionId).toPromise();
  }

  public async syncSendOrder(_model: SalesTransactionView) {
    console.log(_model);
    return await this.http.post<number>(environment.API_SYNC_URL + 'sync-data/postSendOrderData', _model).toPromise();
  }

  public async getTransactionId(_doc_no : string) {
    return await this.http.get<SalesTransactionUpdateStatusView>(environment.API_URL + 'sales/getTransactionId/' + _doc_no).toPromise();
  }

}
