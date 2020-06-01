import { Component, OnInit } from '@angular/core';
import { SalesService } from '../../_service/sales.service';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { MessageService } from '../../_service/message.service';
import { AuthenticationService } from '../../_service/authentication.service';
import { Router } from '@angular/router';
import { CatalogMastView, CatalogMastSearchView } from '../../_model/catalog-mast';
import { CommonSearchView } from '../../_model/common-search-view';

@Component({
  selector: 'app-sales-design',
  templateUrl: './sales-design.component.html',
  styleUrls: ['./sales-design.component.scss']
})
export class SalesDesignComponent implements OnInit {

  constructor(
    private _salesSvc: SalesService,
    private _ddlSvc: DropdownlistService,
    private _msgSvc: MessageService,
    private _authSvc: AuthenticationService,
    private _router: Router
  ) { }

  public model: CatalogMastView = new CatalogMastView();
  public model_search: CatalogMastSearchView = new CatalogMastSearchView();
 
  public data: CommonSearchView<CatalogMastView> = new CommonSearchView<CatalogMastView>();

 

  async ngOnInit() {
    this.data = await this._salesSvc.searchDesign(this.model_search);
    console.log(this.data.datas);
  }

  view(catalog_id,y)
  {
     
      this._router.navigateByUrl('/app/sale/product/'+catalog_id+'/'+y.catalog_color_id);
  }

  close()
  {
    window.history.back();
  }
}
