import { Component, OnInit, Inject, ChangeDetectorRef } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, PageEvent, MatDialog } from '@angular/material';
import { SalesProductSelectedView, SalesSelectTypeView, SalesTransactionView, FontSelectedView, TransactionItemView } from '../../_model/sales';
import { FormBuilder } from '@angular/forms';
import { MessageService } from '../../_service/message.service';
import { AuthenticationService } from '../../_service/authentication.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ShareDataService } from '../../_service/share-data.service';
import { SalesService } from '../../_service/sales.service';
import { CatalogDesignService } from '../../_service/catalog-design.service';
import { CatalogMastView } from '../../_model/catalog-mast';
import { DomSanitizer } from '@angular/platform-browser';


@Component({
  selector: 'app-sales-add-product',
  templateUrl: './sales-add-product.component.html',
  styleUrls: ['./sales-add-product.component.scss']
})
export class SalesAddProductComponent implements OnInit {

  constructor(
    // public dialogRef: MatDialogRef<any>,
    // @Inject(MAT_DIALOG_DATA) public data: SalesProductSelectedView,
    private _salesSvc: SalesService,
    private _catalgDesignSvc: CatalogDesignService,
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService,
    private sanitizer: DomSanitizer, 
    private _router: Router,
    private _dialog: MatDialog,
    private _data: ShareDataService,
    private cdr: ChangeDetectorRef
  ) { 
      // this.catalog_id = this.data.catalog_id;
      // this.catalog_color_id = this.data.catalog_color_id;
      // this.designName = this.data.design_name;
      // this.checkedList = this.data.checkedList;
      // this.emb = this.data.emb;
      // this.color = this.data.color;
   }

  
  public model_sale: SalesTransactionView = new SalesTransactionView();
  public model_item: TransactionItemView = new TransactionItemView();
  //public model_sales : SalesTransactionView = new SalesTransactionView();
  public model_font : FontSelectedView = new FontSelectedView();
  public model_design: CatalogMastView = new CatalogMastView();  
  public checkedList:any;
  public oldSaleList:any;
  public sizeList:any;
  public emb: any = [];
  public color: any = [];
  public catalog_id : any;
  public id : any;
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
  public salesList:any;
  sizeSelected:any;
  typeSelected:any;
  public total_qty:any;
  public total_amt:any;

  public confirmList:any;
  public addProductList:any;
  catalog_size_id:any;
  pdsize_code:any;
  pdsize_name:any;
  prod_code:any;
  prod_tname:any;
  unit_price:any;

  public embroidery: string="";
  public font_name: number = 0;
  public font_color: number = 0;      
  
  public branch_code : string = "";
  public branch_name : string = "";
  public total : any;


  async ngOnInit() {

    //this._data.oldSales.subscribe(message => this.oldSaleList = message)
    //this.model_sale = this.checkedList;

    console.log("checklist");
    console.log(this.oldSaleList);

    this.catalog_id = this._actRoute.snapshot.params.catalog;

    this.catalog_color_id = this._actRoute.snapshot.params.color;
    this.id = this._actRoute.snapshot.params.id;


    if(this.catalog_id != undefined)
    {
      this.model_design = await this._catalgDesignSvc.getInfo(this.catalog_id);
    }
    this.designName = this.model_design.dsgn_name;
    
    this._data.currentMessage.subscribe(message => this.checkedList = message)
    this.model_sale = this.checkedList;

    console.log("add");
    console.log(this.model_sale);
     
    this.emb = await this._salesSvc.getEmbroidery();
    this.color = await this._salesSvc.getColorFont(this.catalog_id); 
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

    
    
      this.model_sale.add_price = this.model_font.add_price;
      this.model_sale.embroidery = this.model_font.embroidery;
      this.model_sale.font_color = this.model_font.font_color;
      this.model_sale.font_color_base64 = this.model_font.font_color_base64;
      this.model_sale.font_name = this.model_font.font_name;
      this.model_sale.font_name_base64 = this.model_font.font_name_base64;
    
    
    this._data.selectedSales.subscribe(message => this.sales = this.model_sale)
    //console.log(this.message);
    this._data.confirmSales(this.sales)
    //console.log(this.sales.length);
    
    this._router.navigateByUrl('/app/sale/sales-add-summary/'+this.catalog_id+"/"+this.catalog_color_id+"/"+this.id);
    // this.salesList = this.checkedList;
    
    // this.confirmList=[];
    // this.sizeSelected=[];
    // this.typeSelected=[];
    // this.total_qty = 0;
    // this.total_amt = 0;

    // for (var i = 0; i < this.salesList.length; i++) {
    //   for(var k = 0;k < this.salesList[i].catalogSize.length; k++)
    //   {
    //     if(this.salesList[i].catalogSize[k].isSelected == true)
    //     {
    //       this.sizeSelected.push(this.salesList[i].catalogSize[k]);
    //     }
    //     //console.log(this.sizeSelected);
    //   }
      
    //   for(var j = 0;j < this.salesList[i].catalogType.length; j++)
    //   {
    //     //console.log(this.salesList[i].catalogType[j].catalog_pic_id);
    //     if(this.salesList[i].catalogType[j].qty > 0)
    //     {
    //       this.typeSelected.push(this.salesList[i].catalogType[j]);
          
    //     }
        
       
        
    //   }
     
      
    // }

    // //Transaction Data

    // this.total_qty = 0;
    // this.total_amt = 0;
    // this.total = 0;

    // for (var i = 0; i < this.salesList.length; i++) {
      
                  
    //   this.catalog_size_id = 0;
    //   this.pdsize_code ="";
    //   this.pdsize_name ="";
    //   this.prod_code = "";
    //   this.prod_tname= "";
    //   this.unit_price = 0;


    //   for (j = 0; j < this.sizeSelected.length; j++)
    //   { 
         
       
    //     if(this.salesList[i].catalog_id == this.sizeSelected[j].catalog_id && this.salesList[i].catalog_type_id == this.sizeSelected[j].catalog_type_id)
    //     {
          
    //       this.catalog_size_id = this.sizeSelected[j].catalog_size_id;
    //       this.pdsize_code = this.sizeSelected[j].pdsize_code;
    //       this.pdsize_name = this.sizeSelected[j].pdsize_name;
    //       this.prod_code = this.sizeSelected[j].prod_code;
    //       this.prod_tname = this.sizeSelected[j].prod_tname;
    //       this.unit_price = this.sizeSelected[j].unit_price;
          
          
    //     }
    //   }

    //   console.log(this.catalog_size_id);
    //   console.log(this.pdsize_code);
      
      
      
    //   //console.log(this.salesList[i]);
    //   for (k = 0; k < this.typeSelected.length;k++)
    //   {
    //     this.model_item = new TransactionItemView();
    //     // console.log('sale catalog id: '+this.salesList[i].catalog_id);
    //     // console.log('type : '+this.typeSelected[k].catalog_id);

    //     // console.log('sales catalog type id : '+this.salesList[i].catalog_type_id);
    //     // console.log('type : '+this.typeSelected[k].catalog_type_id);


    //     if(this.salesList[i].catalog_id == this.typeSelected[k].catalog_id && this.salesList[i].catalog_type_id == this.typeSelected[k].catalog_type_id)
    //     {
            
    //         this.model_item.catalog_pic_id = this.typeSelected[k].catalog_pic_id;
    //          this.model_item.catalog_type_code = this.typeSelected[k].catalog_type_code;
    //         this.model_item.type_base64 = this.typeSelected[k].pic_base64;
    //         this.model_item.qty = this.typeSelected[k].qty;
    //         //this.total_qty = this.total_qty + this.typeSelected[k].qty;

    //         this.model_item.catalog_id = this.salesList[i].catalog_id;
    //         this.model_item.catalog_color_id = this.salesList[i].catalog_color_id;
    //         this.model_item.catalog_type_id = this.salesList[i].catalog_type_id;
    //         this.model_item.pdtype_code = this.salesList[i].pdtype_code;
    //         this.model_item.pdtype_tname = this.salesList[i].pdtype_tname;
    //         this.model_item.is_border = this.salesList[i].is_border;
    //         this.model_item.size_sp = this.salesList[i].size_sp;
    //         this.model_item.color_base64 = this.salesList[i].pic_color;
            
    //         this.model_item.amt  = this.model_item.qty * this.model_item.unit_price;
    //         this.model_item.remark = this.salesList[i].remark;

    //         this.model_item.catalog_size_id = this.catalog_size_id;
    //         this.model_item.pdsize_code = this.pdsize_code;
    //         this.model_item.pdsize_name = this.pdsize_name;
    //         this.model_item.prod_code = this.prod_code;
    //         this.model_item.prod_tname = this.prod_tname;
    //         this.model_item.unit_price = this.unit_price;
    //         this.model_item.amt = this.model_item.unit_price * this.model_item.qty ;
            
    //         this.total_qty = this.total_qty + this.model_item.qty;
    //         this.total_amt = this.total_amt + this.model_item.amt;

    //         this.model_sale.branch_code = this.branch_code;
    //         this.model_sale.branch_name = this.branch_name;
    //         this.model_sale.catalog_id = this.salesList[i].catalog_id;

    //         if(this.model_item.catalog_type_code == 'A')
    //         {
    //           this.model_item.embroidery  = "";
    //           this.model_item.font_name = 0;
    //           this.model_item.font_name_base64 = "";
    //           this.model_item.font_color = 0;
    //           this.model_item.font_color_base64 = "";
    //           this.model_item.add_price = 0;

    //           this.embroidery = "";
    //           this.font_name = 0;
    //           this.font_color = 0; 
    //           this.add_price = 0;
              
    //         }
    //         else
    //         {
    //           this.model_item.embroidery  = this.salesList.embroidery;
    //           this.model_item.font_name = this.salesList.font_name;
    //           this.model_item.font_name_base64 = this.salesList.font_name_base64;
    //           this.model_item.font_color = this.salesList.font_color;
    //           this.model_item.font_color_base64 = this.salesList.font_color_base64;
    //           this.model_item.add_price = this.salesList.add_price;

    //           this.embroidery = this.salesList.embroidery;
    //           this.font_name = this.salesList.font_name;
    //           this.font_color = this.salesList.font_color;
    //           this.add_price = this.salesList.add_price;
              
    //         }
    //         this.total = this.total_amt + this.add_price;
          
    //         console.log(this.typeSelected[k].catalog_type_code);
    //         console.log(this.model_item.catalog_type_code);

    //         this.confirmList.push(this.model_item);
    //         // console.log(this.confirmList);
    //     }
         
        
    //   }
    // } 
    
    // this.model_item.add_price = this.model_font.add_price;
    // this.model_item.embroidery = this.model_font.embroidery;
    // this.model_item.font_color = this.model_font.font_color;
    // this.model_item.font_color_base64 = this.model_font.font_color_base64;
    // this.model_item.font_name = this.model_font.font_name;
    // this.model_item.font_name_base64 = this.model_font.font_name_base64;
    // this.model_sale = this.confirmList;

    // this.cdr.detectChanges();
    
    
    // console.log(this.model_sale);
    //this.dialogRef.close(this.model_sale);
    //this.dialogRefSearch.close(this.model);
  }

  close()
  {
    //this.dialogRef.close();
    window.history.back();
  }

}
