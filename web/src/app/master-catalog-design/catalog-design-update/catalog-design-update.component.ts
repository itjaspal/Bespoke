import { Component, OnInit } from '@angular/core';
import { CatalogDesignService } from '../../_service/catalog-design.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../../_service/authentication.service';
import { MessageService } from '../../_service/message.service';
import { CatalogMastView } from '../../_model/catalog-mast';
import { DropdownlistService } from '../../_service/dropdownlist.service';

@Component({
  selector: 'app-catalog-design-update',
  templateUrl: './catalog-design-update.component.html',
  styleUrls: ['./catalog-design-update.component.scss']
})
export class CatalogDesignUpdateComponent implements OnInit {

  constructor(
    private _catalgDesignSvc: CatalogDesignService,
    private _activateRoute: ActivatedRoute,
    private _formBuilder: FormBuilder,
    private _authSvc: AuthenticationService,
    private _ddlSvc: DropdownlistService,
    private _msgSvc: MessageService,
    private _router: Router
  ) { }

  public model: CatalogMastView = new CatalogMastView();
  formGroup: FormGroup;
  public validationForm: FormGroup;
  public code : number = undefined;
  public user: any;
  public ProductBrandLists: any;

  imgURL: any;

  async ngOnInit() {
    this.buildForm();
    this.code = this._activateRoute.snapshot.params.id;
    this.user = this._authSvc.getLoginUser();
    this.ProductBrandLists = await this._ddlSvc.getDdlProductBrand();
    console.log(this.code);
    

    if (this.code != undefined) {
      this.model = await this._catalgDesignSvc.getInfo(this.code);
      this.imgURL = this.model.pic_base64;
      console.log(this.model);
    }
  }

  buildForm() {
    this.validationForm = this._formBuilder.group({
      pdbrnd_code: [null, [Validators.required]],  
      pddsgn_code: [null, [Validators.required]],      
      dsgn_name:[null, [Validators.required]],
      dsgn_desc: [null, []],
      status:[null, [Validators.required]]
    });
  }

  close() {
    window.history.back();
  }

  async save() {

    this.model.pic_base64 = this.imgURL;
    this.model.updated_by = this.user.username;

    await this._catalgDesignSvc.update(this.model);

    await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
    this._router.navigateByUrl('/app/catalog');

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
