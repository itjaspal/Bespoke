import { Component, OnInit } from '@angular/core';
import { CatalogTypeService } from '../../_service/catalog-type.service';
import { CatalogSizeService } from '../../_service/catalog-size.service';
import { CatalogDesignService } from '../../_service/catalog-design.service';
import { MessageService } from '../../_service/message.service';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { DomSanitizer } from '@angular/platform-browser';
import { CatalogSizeView, CatalogSizeSearchView } from '../../_model/catalog-size';
import { CatalogMastView } from '../../_model/catalog-mast';
import { CommonSearchView } from '../../_model/common-search-view';

@Component({
  selector: 'app-catalog-size-create',
  templateUrl: './catalog-size-create.component.html',
  styleUrls: ['./catalog-size-create.component.scss']
})
export class CatalogSizeCreateComponent implements OnInit {

  constructor(
    private _typeSvc: CatalogTypeService,
    private _sizeSvc: CatalogSizeService,
    private _catalgDesignSvc: CatalogDesignService,
    private _msgSvc: MessageService,
    private _ddlSvc: DropdownlistService,
    private _formBuilder: FormBuilder,
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService,
    private sanitizer: DomSanitizer, 
    private _router: Router
  ) { }

  public model: CatalogSizeView = new CatalogSizeView();
  public model_search: CatalogSizeSearchView = new CatalogSizeSearchView();
  public model_design: CatalogMastView = new CatalogMastView();
  public data: CommonSearchView<CatalogSizeView> = new CommonSearchView<CatalogSizeView>();
  imgURL: any;
  formGroup: FormGroup;
  public validationForm: FormGroup;
  public user: any;
  public CatalogTypeLists: any;
  public ProductSizeLists: any;
  public size: any = [];
  public designName : any;

  async ngOnInit() {
    this.buildForm();
    this.user = this._authSvc.getLoginUser();
    this.model_search.catalog_id = this._actRoute.snapshot.params.catalog_id;
    this.ProductSizeLists = await this._ddlSvc.getDdlProductSize();
    this.CatalogTypeLists = await this._ddlSvc.getDdlTypeInCatalog(this.model_search.catalog_id);
    
    if(this.model_search.catalog_id != undefined)
    {
      this.model_design = await this._catalgDesignSvc.getInfo(this.model_search.catalog_id);
    }
    this.designName = this.model_design.dsgn_name;
    
    this.size = await this._sizeSvc.getSizeCatalog(this.model_search.catalog_id);
    console.log(this.size);
  
  }

  buildForm() {
    
    this.validationForm = this._formBuilder.group({
      pdsize_code: [null, [Validators.required]],
      sort_seq: [null, [Validators.required]],
      catalog_type_id: [null, [Validators.required]],
    });
  }

  async save()
  {
    console.log(this.model);
    this.model.catalog_id = this._actRoute.snapshot.params.catalog_id;
    //this.model.catalog_type_id = this.imgURL;
    this.model.created_by = this.user.username;
    this.model.updated_by = this.user.username;
    
    
    if(this.model.catalog_type_id == undefined || this.model.pdsize_code =="" )
    {
      await this._msgSvc.warningPopup("ต้องใส่ข้อมูล");
    }
    else
    {
      await this._sizeSvc.create(this.model);
     
      await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
      
      this._router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
      this._router.navigate(["/app/catalog-size/"+this._actRoute.snapshot.params.catalog_id+"/create"]));
      
    }

    
  }

  async search() {
    //console.log(this.model_search);
    this.model_search.catalog_id = this._actRoute.snapshot.params.catalog_id;
    this.data = await this._sizeSvc.search(this.model_search);
    console.log(this.data);
  }

  async delete(size) {

    this._msgSvc.confirmPopup("ยืนยันลบข้อมูล", async result => {
      if (result) {
        let res: any = await this._sizeSvc.delete(size);

        this._msgSvc.successPopup(res.message);

        this._router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
        this._router.navigate(["/app/catalog-size/"+this._actRoute.snapshot.params.catalog_id+"/create"]));
      }
    })

  }

  async get_filter_type(type)
  {
      console.log(type);
      this.size = await this._sizeSvc.getFilterType(this.model_search.catalog_id,type);
  }


  close() {
    window.history.back();
  }


}
