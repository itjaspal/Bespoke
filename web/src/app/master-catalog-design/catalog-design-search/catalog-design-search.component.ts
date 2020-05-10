import { Component, OnInit } from '@angular/core';
import { CatalogDesignService } from '../../_service/catalog-design.service';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { MessageService } from '../../_service/message.service';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { DomSanitizer } from '@angular/platform-browser';
import { CommonSearchView } from '../../_model/common-search-view';
import { CatalogMastSearchView, CatalogMastView } from '../../_model/catalog-mast';
import { CatalogColorSearchView, CatalogColorView } from '../../_model/catalog-color';
import { CatalogColorService } from '../../_service/catalog-color.service';
import { AttachFileView } from '../../_model/attach-file-view';


@Component({
  selector: 'app-catalog-design-search',
  templateUrl: './catalog-design-search.component.html',
  styleUrls: ['./catalog-design-search.component.scss']
})
export class CatalogDesignSearchComponent implements OnInit {

  constructor(
    private _catalgDesignSvc: CatalogDesignService,
    private _catalogColorSvc: CatalogColorService,
    private _ddlSvc: DropdownlistService,
    private _msgSvc: MessageService,
    private _formBuilder: FormBuilder,
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService,
    private sanitizer: DomSanitizer, 
    private _router: Router
  ) { }

  public model: CatalogMastView = new CatalogMastView();
  public model_search: CatalogMastSearchView = new CatalogMastSearchView();
  public model_search_color: CatalogColorSearchView = new CatalogColorSearchView();
  
  //actions: any = {};
  public data: CommonSearchView<CatalogMastView> = new CommonSearchView<CatalogMastView>();

  public data_color: CommonSearchView<CatalogColorView> = new CommonSearchView<CatalogColorView>();
  public catalogDesignLists: any;

  async ngOnInit() {
    this.catalogDesignLists = await this._ddlSvc.getDdlCatalogDesign();
    console.log(this.catalogDesignLists);
    this.search();
  }

  async search() {
    console.log(this.model_search);
    this.data = await this._catalgDesignSvc.search(this.model_search);
    console.log(this.data.datas); 
  }

  async search_color() {
    // console.log(this.model_search_color);
    // this.data_color = await this._catalogColorSvc.search(this.model_search_color);
    // console.log(this.data.datas); 

    if(this.model_search_color.catalog_id != undefined)
    {
      this._router.navigateByUrl('/app/catalog-color/'+this.model_search_color.catalog_id);
    }
    
    
  }

  view(y) {
    window.open(y);
  }

}
