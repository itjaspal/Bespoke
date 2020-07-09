import { Component, OnInit, Inject, ChangeDetectorRef } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, PageEvent, MatDialog } from '@angular/material';
import { SalesProductSearchView } from '../../_model/sales';
import { FormBuilder } from '@angular/forms';
import { MessageService } from '../../_service/message.service';
import { AuthenticationService } from '../../_service/authentication.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ShareDataService } from '../../_service/share-data.service';
import { SalesAddProductComponent } from '../sales-add-product/sales-add-product.component';
import { SalesService } from '../../_service/sales.service';
import { CatalogDesignService } from '../../_service/catalog-design.service';
import { CatalogMastView } from '../../_model/catalog-mast';


@Component({
  selector: 'app-sales-product-search',
  templateUrl: './sales-product-search.component.html',
  styleUrls: ['./sales-product-search.component.scss']
})
export class SalesProductSearchComponent implements OnInit {

  constructor(
    // public dialogRef: MatDialogRef<any>,
    // @Inject(MAT_DIALOG_DATA) public data: SalesProductSearchView,
    private _fb: FormBuilder,
    private _msg: MessageService,
    private _authSvc: AuthenticationService,
    private _router: Router,
    private _dialog: MatDialog,
    private _data: ShareDataService,
    private _salesSvc: SalesService,
    private _catalgDesignSvc: CatalogDesignService,
    private _msgSvc: MessageService,
    private _actRoute:ActivatedRoute,
    private ref: ChangeDetectorRef
  ) { 
      // this.catalog_id = this.data.catalog_id;
      // this.catalog_color_id = this.data.catalog_color_id;
      // this.designName = this.data.design_name;
      // this.type = this.data.type;
      // this.color = this.data.color;
      // this.emb = this.data.emb;
      // this.color_font = this.data.color_font;
      
  }

  public model_design: CatalogMastView = new CatalogMastView();  
  public type: any = [];
  public color: any = [];
  public emb: any = [];
  public color_font: any = [];
  public catalog_id : any;
  public catalog_color_id : any;
  public id : any;
  public designName : any;

  public checkedList:any;
  public message:any;

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
    console.log(this.type);
    
    
  }

  async ngAfterContentChecked() {
    
    this.ref.detectChanges();
  }

  // radioChange(catalog,color) {
  //   //console.log($event.value);
  //   console.log(catalog);
  //   this._router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
  //     this._router.navigate(["/app/sale/product/"+catalog+"/"+color]));
   
  // }

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
    //this.ref.detectChanges();
    console.log(this.checkedList);
  }

  Confirm()
  {
    this.catalog_id = this._actRoute.snapshot.params.catalog;
    this.catalog_color_id = this._actRoute.snapshot.params.color;
    this.id = this._actRoute.snapshot.params.id;
    //console.log(this.checkedList);
    this._data.currentMessage.subscribe(message => this.message = this.checkedList)
    //console.log(this.message);
    this._data.changeMessage(this.message)
    this._router.navigateByUrl('/app/sale/product-add/'+this.catalog_id+"/"+this.catalog_color_id+"/"+this.id);
  }
  // Confirm()
  // {
  //   //this.dialogRef.close();

  //   const dialogRef = this._dialog.open(SalesAddProductComponent, {
  //     maxWidth: '100vw',
  //     maxHeight: '100vh',
  //     height: '100%',
  //     width: '100%',
  //     data: {
  //       catalog_id: this.catalog_id,
  //       catalog_color_id: this.catalog_color_id,
  //       design_name : this.designName,
  //       checkedList : this.checkedList,
  //       emb : this.emb,
  //       color : this.color_font,
  //     }
  //   });

    

  //   dialogRef.afterClosed().subscribe(result => {
  //     if (result) {
  //       console.log(result);
  //       //let product = result;
  //       //this.model.transactionItem.push(product);
  //       //this.calculate();
  //     }
  //   });

  // }

  close()
  {
    window.history.back();
    //this.dialogRef.close();
  }

}
