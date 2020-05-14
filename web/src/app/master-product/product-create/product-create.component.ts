import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../_service/product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { MessageService } from '../../_service/message.service';
import { ProductAttributeView } from '../../_model/productAttribute';
import { AuthenticationService } from '../../_service/authentication.service';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.scss']
})
export class ProductCreateComponent implements OnInit {

  constructor(
    private _productSvc: ProductService,
    private _activateRoute: ActivatedRoute,
    private _authSvc: AuthenticationService,
    private _formBuilder: FormBuilder,
    private _ddlSvc: DropdownlistService,
    private _msgSvc: MessageService,
    private _router: Router
  ) { }

  public ddlStatus: any;

  public model: ProductAttributeView = new ProductAttributeView();
  public validationForm: FormGroup;
  public user: any;


  async ngOnInit() {
    this.model.productAttributeTypeCode = this._activateRoute.snapshot.params.attr;
    
    this.buildForm();

    //this.ddlStatus = await this._ddlSvc.getDdlBranchStatus();
  }

  buildForm() {
    this.validationForm = this._formBuilder.group({
      code: [null, [Validators.required]],
      name: [null, [Validators.required]],     
      status: [null, [Validators.required]],
    });
  }

  close() {
    window.history.back();
  }

  async save() {
    this.user = this._authSvc.getLoginUser();
    this.model.user_code = this.user.username;
    this.model.code = this.model.code.toUpperCase();
    await this._productSvc.create(this.model);
    console.log(this.model);

    await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
    this._router.navigateByUrl('/app/product');

  }

}
