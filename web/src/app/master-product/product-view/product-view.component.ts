import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../_service/product.service';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { AuthenticationService } from '../../_service/authentication.service';
import { ActivatedRoute } from '@angular/router';
import { ProductSearchView, ProductView } from '../../_model/product';
import { AppSetting } from '../../_constants/app-setting';
import { CommonSearchView } from '../../_model/common-search-view';
import { async } from 'rxjs/internal/scheduler/async';
import { search } from 'core-js/fn/symbol';
import { PageEvent } from '@angular/material';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-product-view',
  templateUrl: './product-view.component.html',
  styleUrls: ['./product-view.component.scss']
})
export class ProductViewComponent implements OnInit {

  constructor(
    private _productSvc: ProductService,
    private _ddlSvc: DropdownlistService,
    private _authSvc: AuthenticationService,
    private _actRoute: ActivatedRoute
  ) { }

  public toppingList: any = [];
  public model: ProductSearchView = new ProductSearchView();
  public ddlStatus: any;
  public ddlProductGroup: any;
  public ddlProductType: any;
  public ddlProductModel: any;
  public ddlProductBrand: any;
  public ddlProductDesign: any;
  public ddlProductColor: any;
  public ddlProductSize: any;
  public ddlProductUom: any; 
  public pageSizeOptions: number[] = AppSetting.pageSizeOptions;
  public pageEvent: any;
  actions: any = {};

  public data: CommonSearchView<ProductView> = new CommonSearchView<ProductView>();
  
  async ngOnInit() {
    //this.actions = this._authSvc.getActionAuthorization(this._actRoute);

    
    this.ddlProductType = await this._ddlSvc.getDdlProductType();
    this.ddlProductBrand = await this._ddlSvc.getDdlProductBrand();
    this.ddlProductDesign = await this._ddlSvc.getDdlProductDesign();
    this.ddlProductColor = await this._ddlSvc.getDdlProductColor();
    this.ddlProductSize = await this._ddlSvc.getDdlProductSize();
    
  

  console.log(this.ddlProductType);

  // if (sessionStorage.getItem('session-product-search') != null) {
  //   this.model = JSON.parse(sessionStorage.getItem('session-product-search'));
  //   this.search();
  // }
  
}

async ngOnDestroy() {
  this.saveSession();
}

async saveSession() {
  sessionStorage.setItem('session-product-search', JSON.stringify(this.model));
}

async search(event: PageEvent = null) {

  if (event != null) {
    this.model.pageIndex = event.pageIndex;
    this.model.itemPerPage = event.pageSize;
  }
  console.log(this.model)

  this.data = await this._productSvc.searchProduct(this.model);
  console.log(this.data);
}

delete(row: ProductView) {

}

}
