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

  async ngOnInit() {

    this.user = this._authSvc.getLoginUser();

    this.saleTransactionId = this._actRoute.snapshot.params.id;
    this.model = await this._salesSvc.getInquirySalesTransactionInfo(this.saleTransactionId);
    if(this.model.catalog_id != undefined)
    {
      this.model_design = await this._catalgDesignSvc.getInfo(this.model.catalog_id);
    }
    this.designName = this.model_design.dsgn_name;

    this.datas = await this._salesSvc.getInquiryAttachFile(this.saleTransactionId);
  }

  view(x: SalesAttachView) {
    window.open(x.fullPath);
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

  close() {
    this._router.navigateByUrl('/app/sale');
  }

}
