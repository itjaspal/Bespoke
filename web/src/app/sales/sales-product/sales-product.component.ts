import { SalesAddComponent } from './../sales-add/sales-add.component';
import { Component, OnInit ,Input } from '@angular/core';
//import { Component, ViewChild, AfterViewInit, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { SalesService } from '../../_service/sales.service';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { MessageService } from '../../_service/message.service';
import { AuthenticationService } from '../../_service/authentication.service';
import { CatalogMastView } from '../../_model/catalog-mast';
import { CatalogDesignService } from '../../_service/catalog-design.service';


//import { MatRadioChange } from '@angular/material';

@Component({
  selector: 'app-sales-product',
  templateUrl: './sales-product.component.html',
  styleUrls: ['./sales-product.component.scss']
})
export class SalesProductComponent implements OnInit {


  
  constructor(
    private _salesSvc: SalesService,
    private _catalgDesignSvc: CatalogDesignService,
    private _ddlSvc: DropdownlistService,
    private _msgSvc: MessageService,
    private _authSvc: AuthenticationService,
    private _actRoute:ActivatedRoute,
    private _router: Router
  ) { }


  public model_design: CatalogMastView = new CatalogMastView();  
  public type: any = [];
  public color: any = [];
  public catalog_id : any;
  public catalog_color_id : any;
  public designName : any;

  public checkedList:any;

  async ngOnInit() {
    this.catalog_id = this._actRoute.snapshot.params.catalog;
    this.catalog_color_id = this._actRoute.snapshot.params.color;

    if(this.catalog_id != undefined)
    {
      this.model_design = await this._catalgDesignSvc.getInfo(this.catalog_id);
    }
    this.designName = this.model_design.dsgn_name;


    this.type = await this._salesSvc.getTypeInCatalogColor(this.catalog_id,this.catalog_color_id);
    this.color = await this._salesSvc.getColorInCatalog(this.catalog_id);
    console.log(this.color);

    

  }

  radioChange(catalog,color) {
    //console.log($event.value);
    console.log(catalog);
    this._router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
      this._router.navigate(["/app/sale/product/"+catalog+"/"+color]));
   
  }

  getCheckedItemList(){
    //console.log(this.type);
    this.checkedList = [];
    for (var i = 0; i < this.type.length; i++) {
      if(this.type[i].catalog_type_id && this.type[i].isSelected == true)
      {
        //this.color[i].user_code = this.user.username;
        this.checkedList.push(this.type[i]);
      }
      
    }
    //this.checkedList = JSON.stringify(this.checkedList);

    
  }
  
  Confirm()
  {
    console.log(this.checkedList);
    
    this._router.navigateByUrl('/app/sale/create');
  }

  // async ngOnDestroy() {
  //   this.saveSession();
  // }

  // async saveSession() {
  //   sessionStorage.setItem('session-checkedList', JSON.stringify(this.checkedList));
  // }
    
  

  close()
  {
    // window.history.back();
    this._router.navigateByUrl('/app/sale/design');
  }
}
