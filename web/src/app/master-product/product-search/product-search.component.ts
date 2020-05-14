import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../_service/product.service';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { ProductAttributeSearchView, ProductAttributeView } from '../../_model/productAttribute';
import { Dropdownlist } from '../../_model/dropdownlist';
import { AppSetting } from '../../_constants/app-setting';
import { CommonSearchView } from '../../_model/common-search-view';
import { PageEvent } from '@angular/material';
import { forkJoin } from 'rxjs';
import { MessageService } from '../../_service/message.service';

@Component({
  selector: 'app-product-search',
  templateUrl: './product-search.component.html',
  styleUrls: ['./product-search.component.scss']
})
export class ProductSearchComponent implements OnInit {

  constructor(
    private _productSvc: ProductService,
    private _ddlSvc: DropdownlistService,
    private _actRoute: ActivatedRoute,
    private _msgSvc: MessageService,
    private _authSvc: AuthenticationService,
    private _router: Router
  ) { }

  public ddlStatus: any;
  public toppingList: any = [];
  public model: ProductAttributeSearchView = new ProductAttributeSearchView();
  public ddlProductAttributeType: Dropdownlist[] = [];
  public pageSizeOptions: number[] = AppSetting.pageSizeOptions;
  public pageEvent: any;
  actions: any = {};

  public data: CommonSearchView<ProductAttributeView> = new CommonSearchView<ProductAttributeView>();

  ngOnInit() {

    forkJoin([
      this._ddlSvc.getDdlProductAttributesTypes()
      //this._ddlSvc.getDdlBranchStatus()
    ]).subscribe(result => {
      console.log(result[0]);
      this.ddlProductAttributeType = result[0];
      //this.ddlStatus = result[1];
    });
    // if (sessionStorage.getItem('session-product-attribute-search') != null) {
    //   this.model = JSON.parse(sessionStorage.getItem('session-product-attribute-search'));
    //   this.search();
    // }
  }
  // async ngOnDestroy() {
  //   this.saveSession();
  // }

  // async saveSession() {
  //   sessionStorage.setItem('session-product-attribute-search', JSON.stringify(this.model));
  // }

  async search(event: PageEvent = null) {

    if (event != null) {
      this.model.pageIndex = event.pageIndex;
      this.model.itemPerPage = event.pageSize;
    }

    console.log(this.model);
    this.data = await this._productSvc.search(this.model);
    
  }

  async add(attr)
  {
    if(attr=="" || attr == null)
    {
      await this._msgSvc.warningPopup("ต้องเลือกขประเภทคุณลักษณะสินค้า");
    }
    else{
      this._router.navigateByUrl('/app/product/create/'+ attr);
    }
  }
  delete(row: ProductAttributeView) {

  }


}
