import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EmbMastSearchView, EmbMastView } from '../_model/emb-mast';
import { CommonSearchView } from '../_model/common-search-view';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmbroideryService {

  constructor(private http: HttpClient) { }

  public async search(_model: EmbMastSearchView) {
    return await this.http.post<CommonSearchView<EmbMastView>>(environment.API_URL + 'emb-mast/postSearch',_model).toPromise();
  }

  public async create(_model: EmbMastView) {
    return await this.http.post<number>(environment.API_URL + 'emb-mast/postCreate', _model).toPromise();
  }

  public async update(_model: EmbMastView) {
    return await this.http.post<number>(environment.API_URL + 'emb-mast/postUpdate', _model).toPromise();
  }

  public async getInfo(_id: number) {
    return await this.http.get<EmbMastView>(environment.API_URL + 'emb-mast/getInfo/' + _id).toPromise();
  }

  public async delete(params) {
    return await this.http.post(environment.API_URL + 'emb-mast/post/Delete',params).toPromise();
  }
}
