import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../_service/product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { MessageService } from '../../_service/message.service';
import { ProductAttributeView } from '../../_model/productAttribute';
import { AuthenticationService } from '../../_service/authentication.service';

@Component({
  selector: 'app-product-update',
  templateUrl: './product-update.component.html',
  styleUrls: ['./product-update.component.scss']
})
export class ProductUpdateComponent implements OnInit {

  constructor(
    private _productSvc: ProductService,
    private _activateRoute: ActivatedRoute,
    private _formBuilder: FormBuilder,
    private _ddlSvc: DropdownlistService,
    private _msgSvc: MessageService,
    private _authSvc: AuthenticationService,
    private _router: Router
  ) { }

  public ddlStatus: any;
  public model: ProductAttributeView = new ProductAttributeView();
  public validationForm: FormGroup;
  public productAttributeId: number = undefined;
  public productAttributeType: string = "";
  public user: any;

  async ngOnInit() {
    this.productAttributeId = this._activateRoute.snapshot.params.id;
    this.productAttributeType = this._activateRoute.snapshot.params.attr;
    
    this.buildForm();

    //this.ddlStatus = await this._ddlSvc.getDdlBranchStatus();

    if (this.productAttributeType == "BRAND" && this.productAttributeId != undefined) {
      this.model = await this._productSvc.getInfoBrand(this.productAttributeId);
    }

    if (this.productAttributeType == "DESIGN" && this.productAttributeId != undefined) {
      this.model = await this._productSvc.getInfoDesign(this.productAttributeId);
    }

    if (this.productAttributeType == "TYPE" && this.productAttributeId != undefined) {
      this.model = await this._productSvc.getInfoType(this.productAttributeId);
    }

    if (this.productAttributeType == "COLOR" && this.productAttributeId != undefined) {
      this.model = await this._productSvc.getInfoColor(this.productAttributeId);
    }

    if (this.productAttributeType == "SIZE" && this.productAttributeId != undefined) {
      this.model = await this._productSvc.getInfoSize(this.productAttributeId);
    }

  }

  buildForm() {
    this.validationForm = this._formBuilder.group({
      code: [null, []],
      name: [null, [Validators.required]],     
      status: [null, [Validators.required]],
    });
  }

  close() {
    window.history.back();
  }

  async save() {

    //console.log(this.model);
    this.user = this._authSvc.getLoginUser();
    this.model.user_code = this.user.username;

    await this._productSvc.update(this.model);

    await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
    this._router.navigateByUrl('/app/product');

  }

}
