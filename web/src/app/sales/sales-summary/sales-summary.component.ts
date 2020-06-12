import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ShareDataService } from '../../_service/share-data.service';
import { AuthenticationService } from '../../_service/authentication.service';
import { SalesService } from '../../_service/sales.service';

@Component({
  selector: 'app-sales-summary',
  templateUrl: './sales-summary.component.html',
  styleUrls: ['./sales-summary.component.scss']
})
export class SalesSummaryComponent implements OnInit {

  constructor(
    private _data: ShareDataService,
    private _salesSvc: SalesService,
    private _authSvc: AuthenticationService,
    private router: Router
  ) { }

  public salesList:any;
  public user: any;
  public branchName : any;
  public docNo: any;

  ngOnInit() {
    this.user = this._authSvc.getLoginUser();
    this.branchName = this.user.branch.branch.branchCode + ' - ' + this.user.branch.branch.branchNameThai;

    
    this.docNo = this._salesSvc.getDocNo(this.user.branch.branchId);
    console.log(this.docNo);
    this._data.selectedSales.subscribe(sales => this.salesList = sales)
    console.log(this.salesList);
    for (var i = 0; i < this.salesList.length; i++) {
      for(var j=0;j<this.salesList[i].catalogType.length; j++)
      {
        if(this.salesList[i].catalogType[j].qty > 0)
        {
          
          console.log(this.salesList[i].catalogType[j].catalog_type_id);
        }
      }
      
      
    }
    
  }

  Confirm()
  {
    this.router.navigateByUrl('/app/sale/summary');
  }

  close()
  {
    window.history.back();
  }

  print()
  {
    // window.open('file:///D:/Angular/Project/Bespoke/web/src/assets/images-prod/tel.pdf');
    window.open('http://192.168.9.50/bespoke/assets/images-prod/order.pdf','_blank');
  }
  points = [];
  signatureImage;

  showImage(data) {
    this.signatureImage = data;
  }

}
