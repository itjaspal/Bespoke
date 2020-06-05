import { SizeCatalogView, SalesSelectTypeView, FontSelectedView } from './../../_model/sales';
import { Component, Input, OnInit, OnChanges } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { DomSanitizer } from '@angular/platform-browser';
import { ShareDataService } from '../../_service/share-data.service';
import { SalesService } from '../../_service/sales.service';

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
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService,
    private sanitizer: DomSanitizer, 
    private _router: Router,
    private _data: ShareDataService
  ) { }
  
  public model : SalesSelectTypeView = new SalesSelectTypeView();
  public model_font : FontSelectedView = new FontSelectedView();
  public checkedList:any;
  public sizeList:any;
  public emb: any = [];
  public color: any = [];
  public catalog_id : any;
  public emb_mast_id : any;
  public catalog_emb_color_id : any;
  public add_price : any;
  
  async ngOnInit() {

    this.catalog_id = this._actRoute.snapshot.params.catalog;
    
    this._data.currentMessage.subscribe(message => this.checkedList = message)
    this.model = this.checkedList;
     
    this.emb = await this._salesSvc.getEmbroidery();
    this.color = await this._salesSvc.getColorFont(this.catalog_id); 
    console.log(this.model);
  }

  radioColorChange(color) {
   
    this.model_font.font_color = color;
  }

  radioFontChange(font) {
    this.model_font.font_name = font.emb_mast_id;
    this.model_font.add_price = font.unit_price;
    //console.log(this.emb_mast_id);
  }

  getCheckedSizeList(){
    console.log(this.model.catalogSize);
    this.sizeList = [];
    // for (var i = 0; i < this.model.catalogSize.length; i++) {
    //   if(this.model.catalogSize[i].isSelected == true) 
    //   {
    //     //this.color[i].user_code = this.user.username;
    //     this.sizeList.push(this.model.catalogSize[i]);
    //   }
      
    // }
    //this.checkedList = JSON.stringify(this.checkedList);

    console.log(this.sizeList);
  }


  Confirm()
  {

    console.log(this.model_font);
    // console.log(this.emb);
    //this.add_price = this.model.add_price;
    //console.log(this.add_price);
    //this._router.navigateByUrl('/app/sale/summary');
  }

  close()
  {
    window.history.back();
  }

}
