import { Component, OnInit } from '@angular/core';
import { CatalogDesignService } from '../../_service/catalog-design.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { MessageService } from '../../_service/message.service';
import { CatalogMastView } from '../../_model/catalog-mast';
import { AuthenticationService } from '../../_service/authentication.service';

@Component({
  selector: 'app-catalog-design-create',
  templateUrl: './catalog-design-create.component.html',
  styleUrls: ['./catalog-design-create.component.scss']
})
export class CatalogDesignCreateComponent implements OnInit {

  constructor(
    private _catalgDesignSvc: CatalogDesignService,
    private _activateRoute: ActivatedRoute,
    private _authSvc: AuthenticationService,
    private _formBuilder: FormBuilder,
    private _ddlSvc: DropdownlistService,
    private _msgSvc: MessageService,
    private _router: Router
  ) { }

  public model: CatalogMastView = new CatalogMastView();
  public validationForm: FormGroup;
  public ProductBrandLists: any;
  // public ProductTypeLists: any;
  public user: any;
  imgURL: any;

  async ngOnInit() {
     
    this.buildForm();
    this.user = this._authSvc.getLoginUser();
    this.ProductBrandLists = await this._ddlSvc.getDdlProductBrand();
    // this.ProductTypeLists = await this._ddlSvc.getDdlProductType();
    console.log(this.ProductBrandLists);
  }

  buildForm() {
    this.validationForm = this._formBuilder.group({
      pdbrnd_code: [null, [Validators.required]],
      pddsgn_code: [null, [Validators.required]],      
      dsgn_name:[null, [Validators.required]],
      dsgn_desc: [null, []],

    });
  }


  close() {
    window.history.back();
  }

  async save() {

    this.model.pic_base64 = this.imgURL;
    this.model.created_by = this.user.username;
    this.model.updated_by = this.user.username;

    if(this.model.pdbrnd_code == "" || this.model.pddsgn_code == "" || this.model.dsgn_name == "")
    {
      await this._msgSvc.warningPopup("ต้องใส่ข้อมูล");
    }
    else
    {
      await this._catalgDesignSvc.create(this.model);

      await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
      this._router.navigateByUrl('/app/catalog'); 
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

}
