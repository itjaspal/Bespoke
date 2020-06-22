import { SizeCatalogView, SalesSelectTypeView, FontSelectedView, SalesTransactionView, TypeCatalogView } from './../../_model/sales';
import { Component, Input, OnInit, OnChanges } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { DomSanitizer } from '@angular/platform-browser';
import { ShareDataService } from '../../_service/share-data.service';
import { SalesService } from '../../_service/sales.service';
import { concat } from 'core-js/fn/array';
import { CatalogDesignService } from '../../_service/catalog-design.service';
import { CatalogMastView } from '../../_model/catalog-mast';

@Component({
  selector: 'app-sales-add',
  templateUrl: './sales-add.component.html',
  styleUrls: ['./sales-add.component.scss']
  //inputs:['checkedList']
})
export class SalesAddComponent implements OnInit {
  //@Input() public checkedList;
  
  constructor(
    private _salesSvc: SalesService,
    private _catalgDesignSvc: CatalogDesignService,
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService,
    private sanitizer: DomSanitizer, 
    private _router: Router,
    private _data: ShareDataService
  ) { }
  
  public model : SalesSelectTypeView = new SalesSelectTypeView();
  public model_sales : SalesTransactionView = new SalesTransactionView();
  public model_font : FontSelectedView = new FontSelectedView();
  public model_design: CatalogMastView = new CatalogMastView();  
  public checkedList:any;
  public sizeList:any;
  public emb: any = [];
  public color: any = [];
  public catalog_id : any;
  public emb_mast_id : any;
  public catalog_emb_color_id : any;
  public add_price : any;
  public sales:any;
  public show_spSize:boolean = false;
  public currentlyChecked : any;
  public CheckBoxType : any;
  //public selected = -1;
  public catalog_color_id : any;
  public designName : any;
  
  async ngOnInit() {

    this.catalog_id = this._actRoute.snapshot.params.catalog;

    if(this.catalog_id != undefined)
    {
      this.model_design = await this._catalgDesignSvc.getInfo(this.catalog_id);
    }
    this.designName = this.model_design.dsgn_name;
    
    this._data.currentMessage.subscribe(message => this.checkedList = message)
    this.model_sales = this.checkedList;
     
    this.emb = await this._salesSvc.getEmbroidery();
    this.color = await this._salesSvc.getColorFont(this.catalog_id); 
    //console.log(this.model);
  }

  radioColorChange(color) {
   
    this.model_font.font_color = color.catalog_emb_color_id;

    this.model_font.font_color_base64 = color.pic_base64;

    //console.log(color);
  }

  radioFontChange(font) {
    this.model_font.font_name = font.emb_mast_id;
    this.model_font.font_name_base64 = font.pic_base64;
    this.model_font.add_price = font.unit_price;
    //console.log(this.emb_mast_id);
  }

  
  getCheckedSizeList(size){

    // for (var i = 0; i < this.sales.length; i++) {
    //   for(var j=0;j<this.sales[i].catalogType.length; j++)
    //   {
    //     if(this.sales[i].catalogType[j].qty > 1)
    //     {
          
    //       console.log("No");
    //     }
    //   }
    // }  
      

   //console.log(size);
    if(size=="OTH")
    {
      this.show_spSize = true;
    }
    else{
      this.show_spSize = false;
    }
    
  }


  Confirm()
  {
    this.catalog_id = this._actRoute.snapshot.params.catalog;
    this.catalog_color_id = this._actRoute.snapshot.params.color;
    this.model_sales.add_price = this.model_font.add_price;
    this.model_sales.embroidery = this.model_font.embroidery;
    this.model_sales.font_color = this.model_font.font_color;
    this.model_sales.font_color_base64 = this.model_font.font_color_base64;
    this.model_sales.font_name = this.model_font.font_name;
    this.model_sales.font_name_base64 = this.model_font.font_name_base64;

    
    this._data.selectedSales.subscribe(message => this.sales = this.model_sales)
    //console.log(this.message);
    this._data.confirmSales(this.sales)
    //console.log(this.sales.length);
    
    this._router.navigateByUrl('/app/sale/summary/'+this.catalog_id+"/"+this.catalog_color_id);
  }

  close()
  {
    window.history.back();
  }

}
