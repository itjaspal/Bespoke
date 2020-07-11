import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../_service/product.service';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { AuthenticationService } from '../../_service/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductSyncSearchView } from '../../_model/product';
import { ImportDataService } from '../../_service/import-data.service';
import { MessageService } from '../../_service/message.service';
import { ImportProductView, DatasProductView } from '../../_model/import-data';

@Component({
  selector: 'app-product-sync',
  templateUrl: './product-sync.component.html',
  styleUrls: ['./product-sync.component.scss']
})
export class ProductSyncComponent implements OnInit {

  constructor(
    private _productSvc: ProductService,
    private _ddlSvc: DropdownlistService,
    private _authSvc: AuthenticationService,
    private _actRoute: ActivatedRoute,
    private importSvc: ImportDataService,
    private _msgSvc: MessageService,
    private _router: Router
  ) { }

  public model_search: ProductSyncSearchView = new ProductSyncSearchView();
  public model: ImportProductView = new ImportProductView();
  public dataList : DatasProductView = new DatasProductView();
  public ddlProductDesign: any;
  public data : any;
  public importList : any;

  async ngOnInit() {
    this.ddlProductDesign = await this._ddlSvc.getDdlProductDesign();
  }

  async sync()
  {

    this.data = await this._productSvc.syncProduct(this.model_search);

    console.log(this.data);
    //this.model.product = this.data;
    //console.log(this.model);
    this.importList = [];
    console.log(this.importList);

    for(var i=0;i<this.data.length;i++)
    {
      
      this.dataList.prod_code = this.data[i].prod_code;
      this.dataList.prod_name = this.data[i].prod_name;
      this.dataList.uom_code  = this.data[i].uom_code;
      this.dataList.bar_code  = this.data[i].bar_code;
      this.dataList.entity  = this.data[i].entity;
      this.dataList.pdgrp_code  = this.data[i].pdgrp_code;
      this.dataList.pdbrnd_code  = this.data[i].pdbrnd_code;
      this.dataList.pdtype_code  = this.data[i].pdtype_code;
      this.dataList.pddsgn_code  = this.data[i].pddsgn_code;
      this.dataList.pdsize_code  = this.data[i].pdsize_code;
      this.dataList.pdcolor_code  = this.data[i].pdcolor_code;
      this.dataList.pdmisc_code  = this.data[i].pdmisc_code;
      this.dataList.pdmodel_code  = this.data[i].pdmodel_code;
      this.dataList.pdgrp_desc  = this.data[i].pdgrp_desc;
      this.dataList.pdbrnd_desc  = this.data[i].pdbrnd_desc; 
      this.dataList.pdtype_desc  = this.data[i].pdtype_desc;
      this.dataList.pddsgn_desc  = this.data[i].pddsgn_desc;
      this.dataList.pdcolor_desc  = this.data[i].pdcolor_desc;
      this.dataList.pdsize_desc  = this.data[i].pdsize_desc;
      this.dataList.pdmisc_desc  = this.data[i].pdmisc_desc;
      this.dataList.pdmodel_desc  = this.data[i].pdmodel_desc;
      this.dataList.unit_price  = this.data[i].unit_price;
      
      console.log(this.importList);
      this.importList.push(this.dataList);
      this.dataList =  new DatasProductView();
      //console.log(this.importList);
    }

    //console.log(this.importList); 

    this.model.product = this.importList;
    
    await this.importSvc.importProduct(this.model);

     await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
     this._router.navigateByUrl('/app/product/sync');
    
  }

}
