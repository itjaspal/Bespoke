import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../_service/product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { MessageService } from '../../_service/message.service';
import { AuthenticationService } from '../../_service/authentication.service';
import { ProductView } from '../../_model/product';

@Component({
  selector: 'app-product-update-price',
  templateUrl: './product-update-price.component.html',
  styleUrls: ['./product-update-price.component.scss']
})
export class ProductUpdatePriceComponent implements OnInit {

  constructor(
    private _productSvc: ProductService,
    private _activateRoute: ActivatedRoute,
    private _formBuilder: FormBuilder,
    private _ddlSvc: DropdownlistService,
    private _msgSvc: MessageService,
    private _authSvc: AuthenticationService,
    private _router: Router
  ) { }

  public toppingList: any = [];
  public model: ProductView = new ProductView();
  public ddlStatus: any;
  public ddlProductGroup: any;
  public ddlProductType: any;
  public ddlProductModel: any;
  public ddlProductBrand: any;
  public ddlProductDesign: any;
  public ddlProductColor: any;
  public ddlProductSize: any;
  public ddlProductUom: any; 
  
  public validationForm: FormGroup;
  public id: number = undefined;
  
  actions: any = {};

  async ngOnInit() {
    this.buildForm();
    this.id = this._activateRoute.snapshot.params.id;
    this.model = await this._productSvc.getInfoProduct(this.id);

    this.ddlProductType = await this._ddlSvc.getDdlProductType();
    this.ddlProductBrand = await this._ddlSvc.getDdlProductBrand();
    this.ddlProductDesign = await this._ddlSvc.getDdlProductDesign();
    this.ddlProductColor = await this._ddlSvc.getDdlProductColor();
    this.ddlProductSize = await this._ddlSvc.getDdlProductSize();
  }

  buildForm() {
    this.validationForm = this._formBuilder.group({
      prod_code: [null, []],
      prod_tname: [null, [Validators.required]],     
      prod_status: [null, [Validators.required]],
    });
  }

  close() {
    window.history.back();
  }

  save()
  {

  }

}
