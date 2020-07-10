import { Injectable } from '@angular/core';
import { ImportDataView, ImportProductView } from '../_model/import-data';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ImportDataService {

  constructor(private http: HttpClient) { }

  public async importDesign(_model: ImportDataView) {
    return await this.http.post<number>(environment.API_URL + 'import-data/postImportDesign', _model).toPromise();
  }

  public async importType(_model: ImportDataView) {
    return await this.http.post<number>(environment.API_URL + 'import-data/postImportType', _model).toPromise();
  }

  public async importColor(_model: ImportDataView) {
    return await this.http.post<number>(environment.API_URL + 'import-data/postImportColor', _model).toPromise();
  }

  public async importSize(_model: ImportDataView) {
    return await this.http.post<number>(environment.API_URL + 'import-data/postImportSize', _model).toPromise();
  }

  public async importProduct(_model: ImportProductView) {
    return await this.http.post<number>(environment.API_URL + 'import-data/postImportProduct', _model).toPromise();
  }
}
