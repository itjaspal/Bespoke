import { Component, OnInit } from '@angular/core';
import { CatalogTypeService } from '../../_service/catalog-type.service';
import { CatalogDesignService } from '../../_service/catalog-design.service';
import { MessageService } from '../../_service/message.service';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { DomSanitizer } from '@angular/platform-browser';
import { CatalogTypeView } from '../../_model/catalog-type';
import { CatalogMastView } from '../../_model/catalog-mast';

@Component({
  selector: 'app-catalog-type-update',
  templateUrl: './catalog-type-update.component.html',
  styleUrls: ['./catalog-type-update.component.scss']
})
export class CatalogTypeUpdateComponent implements OnInit {

  constructor(
    private _typeSvc: CatalogTypeService,
    private _catalgDesignSvc: CatalogDesignService,
    private _msgSvc: MessageService,
    private _ddlSvc: DropdownlistService,
    private _formBuilder: FormBuilder,
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService,
    private sanitizer: DomSanitizer, 
    private _router: Router
  ) { }

  public model: CatalogTypeView = new CatalogTypeView();
  // public model_search: CatalogTypeSearchView = new CatalogTypeSearchView();
   public model_design: CatalogMastView = new CatalogMastView();
  // public data: CommonSearchView<CatalogTypeView> = new CommonSearchView<CatalogTypeView>();
  imgURL: any;
  formGroup: FormGroup;
  public validationForm: FormGroup;
  public user: any;
  public ProductTypeLists: any;
  public catalogColorLists: any;
  public type: any = [];
  public designName : any;
  public code : number = undefined;
  public catalog_id : number = undefined;

  async ngOnInit() {
    this.buildForm();
    this.code = this._actRoute.snapshot.params.id;
    this.catalog_id = this._actRoute.snapshot.params.catalog_id;
    this.user = this._authSvc.getLoginUser();
    this.ProductTypeLists = await this._ddlSvc.getDdlProductType();
    //this.catalogColorLists = await this._ddlSvc.getDdlColorInCatalog(this.catalog_id);
    
    if(this.catalog_id != undefined)
    {
      this.model_design = await this._catalgDesignSvc.getInfo(this.catalog_id);
    }
    this.designName = this.model_design.dsgn_name;

    if (this.code != undefined) {
      this.model = await this._typeSvc.getInfo(this.code);
      //this.imgURL = this.model.pic_base64;
      console.log(this.model);
    }
  }

  buildForm() {
    
    this.validationForm = this._formBuilder.group({
      pdtype_code: [null, [Validators.required]],
      sort_seq: [null, [Validators.required]],
      // catalog_color_id: [null, [Validators.required]],
      // catalog_type_code: [null, [Validators.required]],
      // type_sort_seq: [null, [Validators.required]],
      is_border: [null, []],    
      status: [null, [Validators.required]],
    
      
    });
  }
  close() {
    window.history.back();
  }

  async save() {

    this.model.pic_base64 = this.imgURL;
    this.model.updated_by = this.user.username;

    console.log(this.model);

    await this._typeSvc.update(this.model);

    await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
    this._router.navigateByUrl('/app/catalog-type/'+this.model.catalog_id);

  }

  preview(event) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]); // read file as data url

      reader.onload = (event) => { // called once readAsDataURL is complete
        //this.imgURL = event.target.result;
        this.imgURL = reader.result;

        console.log(this.imgURL);
      }

    }
  }


}
