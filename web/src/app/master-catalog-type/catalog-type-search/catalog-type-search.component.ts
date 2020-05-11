import { Component, OnInit } from '@angular/core';
import { CatalogTypeService } from '../../_service/catalog-type.service';
import { CatalogSizeService } from '../../_service/catalog-size.service';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { MessageService } from '../../_service/message.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { DomSanitizer } from '@angular/platform-browser';
import { CatalogTypeView, CatalogTypeSearchView } from '../../_model/catalog-type';
import { CatalogSizeView, CatalogSizeSearchView } from '../../_model/catalog-size';
import { CommonSearchView } from '../../_model/common-search-view';
import { CatalogDesignService } from '../../_service/catalog-design.service';
import { CatalogMastView } from '../../_model/catalog-mast';

@Component({
  selector: 'app-catalog-type-search',
  templateUrl: './catalog-type-search.component.html',
  styleUrls: ['./catalog-type-search.component.scss']
})
export class CatalogTypeSearchComponent implements OnInit {

  constructor(
    private _catalogTypeSvc: CatalogTypeService,
    private _catalogSizeSvc: CatalogSizeService,
    private _catalgDesignSvc: CatalogDesignService,
    private _ddlSvc: DropdownlistService,
    private _msgSvc: MessageService,
    private _formBuilder: FormBuilder,
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService,
    private sanitizer: DomSanitizer, 
    private _router: Router
  ) { }

  public model: CatalogTypeView = new CatalogTypeView();
  public model_search: CatalogTypeSearchView = new CatalogTypeSearchView();
  public model_design: CatalogMastView = new CatalogMastView();
  public model_size: CatalogSizeView = new CatalogSizeView();
  public model_size_search: CatalogSizeSearchView = new CatalogSizeSearchView();
  
  //actions: any = {};
  public data: CommonSearchView<CatalogTypeView> = new CommonSearchView<CatalogTypeView>();
  public validationForm: FormGroup;
  
  public catalogDesignLists: any;
  public designName : any;

  async ngOnInit() {
    this.buildForm();
    this.model_search.catalog_id = this._actRoute.snapshot.params.catalog_id;
    this.catalogDesignLists = await this._ddlSvc.getDdlCatalogDesign();
  
    //console.log(this.model.catalog_id);
   
    this.data = await this._catalogTypeSvc.search(this.model_search);

    if(this.model_search.catalog_id != undefined)
    {
      this.model_design = await this._catalgDesignSvc.getInfo(this.model_search.catalog_id);
    }
    this.designName = this.model_design.dsgn_name;

    //this.catalog_id.nativeElement.value = this.model_search.catalog_id;
    this.search();
  }

  buildForm() {
    this.validationForm = this._formBuilder.group({
      catalog_id: [null, [Validators.required]],
    });
  }

  async search() {
    console.log(this.model_search);
    //this.model_search.catalog_id = this.model.catalog_id;
    this.data = await this._catalogTypeSvc.search(this.model_search);
    console.log(this.data); 
  }

  async add_type() {
    this._router.navigateByUrl('/app/catalog-type/'+this.model_search.catalog_id+'/create');
  }

  async add_size() {
    this._router.navigateByUrl('/app/catalog-size/'+this.model_size_search.catalog_id+'/create');
  }

  async color_search() {
    this._router.navigateByUrl('/app/catalog-color/'+this.model_search.catalog_id);
  }
  
  async emb_search() {
    this._router.navigateByUrl('/app/catalog-emb/'+this.model_search.catalog_id);
  }

  async border_search() {
    this._router.navigateByUrl('/app/catalog-border/'+this.model_search.catalog_id);
  }

  async type_search() {
    this._router.navigateByUrl('/app/catalog-type/'+this.model_search.catalog_id);
  }



  async delete_type(color) {
    console.log(color); 

    this._msgSvc.confirmPopup("ยืนยันลบข้อมูล", async result => {
      if (result) {
        let res: any = await this._catalogTypeSvc.delete(color);

        this._msgSvc.successPopup(res.message);

        await this.search();
      }
    })

  }


  async delete_size(color) {
    console.log(color); 

    this._msgSvc.confirmPopup("ยืนยันลบข้อมูล", async result => {
      if (result) {
        let res: any = await this._catalogSizeSvc.delete(color);

        this._msgSvc.successPopup(res.message);

        await this.search();
      }
    })

  }

  close() {
    this._router.navigateByUrl('/app/catalog');
  }

}
