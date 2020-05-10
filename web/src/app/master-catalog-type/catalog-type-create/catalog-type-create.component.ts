import { Component, OnInit } from '@angular/core';
import { CatalogTypeService } from '../../_service/catalog-type.service';
import { MessageService } from '../../_service/message.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { DomSanitizer } from '@angular/platform-browser';
import { CatalogTypeView, CatalogTypeSearchView } from '../../_model/catalog-type';
import { CommonSearchView } from '../../_model/common-search-view';
import { DropdownlistService } from '../../_service/dropdownlist.service';

@Component({
  selector: 'app-catalog-type-create',
  templateUrl: './catalog-type-create.component.html',
  styleUrls: ['./catalog-type-create.component.scss']
})
export class CatalogTypeCreateComponent implements OnInit {

  constructor(
    private _typeSvc: CatalogTypeService,
    private _msgSvc: MessageService,
    private _ddlSvc: DropdownlistService,
    private _formBuilder: FormBuilder,
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService,
    private sanitizer: DomSanitizer, 
    private _router: Router
  ) { }

  public model: CatalogTypeView = new CatalogTypeView();
  public model_search: CatalogTypeSearchView = new CatalogTypeSearchView();
  public data: CommonSearchView<CatalogTypeView> = new CommonSearchView<CatalogTypeView>();
  imgURL: any;
  formGroup: FormGroup;
  public validationForm: FormGroup;
  public user: any;
  public ProductTypeLists: any;
  public catalogColorLists: any;

  async ngOnInit() {
    this.buildForm();
    this.user = this._authSvc.getLoginUser();
    this.model_search.catalog_id = this._actRoute.snapshot.params.catalog_id;
    this.ProductTypeLists = await this._ddlSvc.getDdlProductType();
    this.catalogColorLists = await this._ddlSvc.getDdlColorInCatalog(this.model_search.catalog_id);
    //this.search();
  }

  buildForm() {
    
    this.validationForm = this._formBuilder.group({
      pdtype_code: [null, [Validators.required]],
      sort_seq: [null, [Validators.required]],
      catalog_color_id: [null, [Validators.required]],
      isBorder: [null, []],    
    
      
    });
  }

  async search() {
    //console.log(this.model_search);
    this.data = await this._typeSvc.search(this.model_search);
    console.log(this.data);
  }

  async save()
  {
    console.log(this.model);
    this.model.pic_base64 = this.imgURL;
    this.model.created_by = this.user.username;
    this.model.updated_by = this.user.username;

    if(this.model.pdtype_code == "" )
    {
      await this._msgSvc.warningPopup("ต้องใส่ข้อมูล");
    }
    else
    {
      await this._typeSvc.create(this.model);
     
      await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
      await this.search();
      //this._router.navigateByUrl('/app/color-font');
    }

    
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


  async delete(color) {

    this._msgSvc.confirmPopup("ยืนยันลบข้อมูล", async result => {
      if (result) {
        let res: any = await this._typeSvc.delete(color);

        this._msgSvc.successPopup(res.message);

        await this.search();
      }
    })

  }

}
