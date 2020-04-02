import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sales-summary',
  templateUrl: './sales-summary.component.html',
  styleUrls: ['./sales-summary.component.scss']
})
export class SalesSummaryComponent implements OnInit {

  constructor(
    private router: Router
  ) { }

  ngOnInit() {
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