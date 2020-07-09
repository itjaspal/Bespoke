import { Component, OnInit } from '@angular/core';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { MessageService } from '../../_service/message.service';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { DomSanitizer } from '@angular/platform-browser';
import { SalesService } from '../../_service/sales.service';
import { SalesView, SalesSearchView, SalesTransactionView } from '../../_model/sales';
import { CommonSearchView } from '../../_model/common-search-view';

@Component({
  selector: 'app-sales-search',
  templateUrl: './sales-search.component.html',
  styleUrls: ['./sales-search.component.scss']
})
export class SalesSearchComponent implements OnInit {

  constructor(
    private _salesSvc: SalesService,
    private _ddlSvc: DropdownlistService,
    private _msgSvc: MessageService,
    private _formBuilder: FormBuilder,
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService,
    private sanitizer: DomSanitizer, 
    private _router: Router
  ) { }

  public model: SalesView = new SalesView();
  public model_search: SalesSearchView = new SalesSearchView();  
  public data: CommonSearchView<SalesView> = new CommonSearchView<SalesView>();

  public branchLists: any;
  public docStatusLists: any;
  public user: any;

  async ngOnInit() {
    this.user = this._authSvc.getLoginUser();
    this.branchLists = await this._ddlSvc.getDdlUserBranch(this.user.username);
    this.docStatusLists = await this._ddlSvc.getDdlDocStatus();
    this.model_search.entity_code = this.user.branch.branch.branchCode;
    console.log(this.user.branch.branch.branchCode);
    this.search();
  }

  async search() {
    console.log(this.model_search);
    this.data = await this._salesSvc.search(this.model_search);
    console.log(this.data.datas); 
  }
  


  async cancel(_row: SalesTransactionView) {
    this._msgSvc.confirmPopup("ยืนยันยกเลิกรายการขาย", async result => {

      if (result) {
        await this._salesSvc.postCancelSalesTransaction({
          co_trns_mast_id: _row.co_trns_mast_id,
          userId: this.user.username
        });

        await this._msgSvc.successPopup("ยกเลิกรายการเรียบร้อย");
        this.search();
      }
    });
  }


  
}
