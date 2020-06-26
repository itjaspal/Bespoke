import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { BaseComponent } from '../../_common/base.component';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { AuthenticationService } from '../../_service/authentication.service';
import { MessageService } from '../../_service/message.service';
import { ActivatedRoute } from '@angular/router';
import { ReportService } from '../../_service/report.service';

@Component({
  selector: 'app-daily-sales-report',
  templateUrl: './daily-sales-report.component.html',
  styleUrls: ['./daily-sales-report.component.scss']
})
export class DailySalesReportComponent implements OnInit {

  public model: any = {
    fromDate: new Date(),
    toDate: new Date(),
    //branchGroupId: 0,
    //branchId: 0,
    entity_code: ""
  }
  public data: any = {
    saleTransactionReports: []
  };
  public branchGroupLists: any;
  public branchLists: any;
  actions: any = {};
  user: any = {};

  @ViewChild("tb_export") private tbExports: ElementRef;

  constructor(
    private _reportService: ReportService,
    private _ddlSvc: DropdownlistService,
    private _authSvc: AuthenticationService,
    public messageService: MessageService,
    private _actRoute: ActivatedRoute
  ) { }

  async ngOnInit() {
    this.actions = this._authSvc.getActionAuthorization(this._actRoute);
    this.user = this._authSvc.getLoginUser();
    this.branchLists = await this._ddlSvc.getDdlUserBranch(this.user.username);

    this.model.entity_code = this.user.branch.branch.branchCode;

    //#region select one
    // if (this.user.username == "admin") {
    //   this.branchGroupLists = await this._ddlSvc.getDdlBranchGroup();
    // } else {
    //   this.branchGroupLists = this._authSvc.getUserBranchGroupes();
    // }
    // if (this.branchGroupLists.length > 0) {
    //   this.model.branchGroupId = this.branchGroupLists[0].key;
    //   if (this.user.username == "admin") {
    //     this.branchLists = await this._ddlSvc.getDdlBranchInGroupRpt(this.model.branchGroupId);
    //   } else {
    //     this.branchLists = this._authSvc.getUserBranches(this.model.branchGroupId);
    //   }
    //   if (this.branchLists.length > 0 ) {
    //     this.model.branchId = this.branchLists[0].key;
    //   }
    // }
  }

  // async getBranchInGroup(branchGroupId: number) {
  //   this.model.branchId = undefined;
  //   if (this.user.username == "admin") {
  //     this.branchLists = await this._ddlSvc.getDdlBranchInGroupRpt(branchGroupId);
  //   } else {
  //     this.branchLists = this._authSvc.getUserBranches(branchGroupId);
  //   }
  //   if (this.branchLists.length > 0) {
  //     this.model.branchId = this.branchLists[0].key;
  //   }
  // }

  async search() {
    if (this.model.entity_code == "" ) {
      this.messageService.errorPopup('กรุณาเลือก กลุ่มห้าง');
      return;
    }
    this.data = await this._reportService.dailySalesReport(this.model);
    //console.log(this.data);
  }

  async clearSearchResult() {
    this.data = {
      saleTransactionReports: []
    };
    console.log("CLEAR");
  }

  print() {
    let head = document.head;
    let style = document.createElement('style');
    style.type = 'text/css';
    style.media = 'print';

    style.appendChild(document.createTextNode('@page { size: A4 landscape; margin: 4mm 0;}'));

    head.appendChild(style);

    window.print();
  }

  export() {

    let title = "";
    if (this.model.reportType == '1') {
      title = "รายงานสรุปยอดขาย";
    } else if (this.model.reportType == '2') {
      title = "รายงานสรุปรายการขาย";
    } else {
      title = "รายงานรายละเอียดการขาย";
    }
    
    BaseComponent.exportToCSV(title, this.tbExports, "saleTransactionReport");

  }

}
