import { SalesAttachView } from './../../_model/sales';
import { Component, OnInit } from '@angular/core';
import { SalesService } from '../../_service/sales.service';
import { CatalogDesignService } from '../../_service/catalog-design.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { MessageService } from '../../_service/message.service';
import { SalesTransactionView } from '../../_model/sales';
import { CatalogMastView } from '../../_model/catalog-mast';

@Component({
  selector: 'app-sales-view',
  templateUrl: './sales-view.component.html',
  styleUrls: ['./sales-view.component.scss']
})
export class SalesViewComponent implements OnInit {

  constructor(
    private _salesSvc: SalesService,
    private _catalgDesignSvc: CatalogDesignService,
    private _actRoute: ActivatedRoute,
    private _router: Router,
    private _authSvc: AuthenticationService,
    private _msgSvc: MessageService
  ) { }

  public saleTransactionId: number = 0;
  public user: any;
  public model: SalesTransactionView = new SalesTransactionView();
  public model_design: CatalogMastView = new CatalogMastView(); 
  public model_attach: SalesAttachView = new SalesAttachView();
  actions: any = {};
  public designName : any;
  datas: any;
  total : any = 0;
  checkAttach : any;

  async ngOnInit() {

    this.user = this._authSvc.getLoginUser();

    
    this.saleTransactionId = this._actRoute.snapshot.params.id;
    
    this.model = await this._salesSvc.getInquirySalesTransactionInfo(this.saleTransactionId);
    console.log(this.model);
    this.total = this.model.total_amt + this.model.add_price;
    if(this.model.catalog_id != undefined)
    {
      this.model_design = await this._catalgDesignSvc.getInfo(this.model.catalog_id);
    }
    this.designName = this.model_design.dsgn_name;

    this.datas = await this._salesSvc.getInquiryAttachFile(this.saleTransactionId);

    // console.log(this.datas);
    
  }

  view(x: SalesAttachView) {
    window.open(x.fullPath);
  }

  
  async delete(x: SalesAttachView) {
    
    console.log(x);
    this._msgSvc.confirmPopup("ยืนยันลบข้อมูล", async result => {
      if (result) {
        let res: any = await this._salesSvc.deleteAttachFile(x);

        this._msgSvc.successPopup(res.message);

        this._router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
        this._router.navigate(["/app/sale/view/"+ x.co_trns_mast_id]));
      }
    })

  }

  print() {
    let head = document.head;
    let style = document.createElement('style');
    style.type = 'text/css';
    style.media = 'print';

    // style.appendChild(document.createTextNode('@page { size: A4 landscape; margin: 4mm 0;}'));
    style.appendChild(document.createTextNode('@page { size: A4 portiate; margin: 4mm 0;}'));

    head.appendChild(style);

    window.print();
  }

  async sendToReady() {


    this.saleTransactionId = this._actRoute.snapshot.params.id;
    // console.log(this.saleTransactionId);

    this.checkAttach = await this._salesSvc.getCheckAttach(this.saleTransactionId);
    // console.log(this.checkAttach);
    if(this.checkAttach == true)
    {
      this._msgSvc.warningPopup("ยังไม่มีการ Attach File");
    }
    else
    {
      this._msgSvc.confirmPopup("ยืนยันส่งรายการขาย", async result => {
        if (result) {
  
          // console.log(this.model);
         
          this.model.font_name_base64 = "";
          this.model.sign_customer = "";
          this.model.sign_manager = "";
          this.model.font_color_base64 = "";
          
          for(var x = 0;x<this.model.transactionItem.length; x++)
          {
            this.model.transactionItem[x].color_base64 = "";
            this.model.transactionItem[x].font_color_base64 = "";
            this.model.transactionItem[x].font_name_base64 = "";
            this.model.transactionItem[x].type_base64 = "";

          }
            
          console.log(this.model);
          await this._salesSvc.syncSendOrder(this.model);
  
          await this._salesSvc.postUpdateToReady({
            co_trns_mast_id: this.saleTransactionId,
            userId: this.user.username
          });
  
          await this._msgSvc.successPopup("ส่งรายการเรียบร้อย");
          this._router.navigateByUrl('/app/sale');
        }
      });
    
    }
   
    
  }

  close() {
    this._router.navigateByUrl('/app/sale');
  }

}
