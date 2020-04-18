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

@Component({
  selector: 'app-catalog-design-search',
  templateUrl: './catalog-design-search.component.html',
  styleUrls: ['./catalog-design-search.component.scss']
})
export class CatalogDesignSearchComponent implements OnInit {

  constructor(
    private _catalgDesignSvc: CatalogDesignService,
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
  //actions: any = {};
  public data: CommonSearchView<CatalogMastView> = new CommonSearchView<CatalogMastView>();
  public catalogDesignLists: any;

  async ngOnInit() {
    this.catalogDesignLists = await this._ddlSvc.getDdlCatalogDesign();
    console.log(this.catalogDesignLists);
  }

}
