import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ColorFontView, ColorFontSearchView } from '../_model/color-font';
import { CommonSearchView } from '../_model/common-search-view';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class ColorFontService {
 
  constructor(private http: HttpClient) { }

  public async search(_model: ColorFontSearchView) { 
    return await this.http.post<CommonSearchView<ColorFontView>>(environment.API_URL + 'color-font/postSearch',_model).toPromise();
  }

  public async create(_model: ColorFontView) {
    return await this.http.post<number>(environment.API_URL + 'color-font/postCreate', _model).toPromise();
  }

  public async update(_model: ColorFontView) {
    return await this.http.post<number>(environment.API_URL + 'color-font/postUpdate', _model).toPromise();
  }

  public async getInfo(_id: number) {
    return await this.http.get<ColorFontView>(environment.API_URL + 'color-font/getInfo/' + _id).toPromise();
  }

  public async delete(params) {
    return await this.http.post(environment.API_URL + 'color-font/post/Delete',params).toPromise();
  }

}
