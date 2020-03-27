import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sales-product',
  templateUrl: './sales-product.component.html',
  styleUrls: ['./sales-product.component.scss']
})
export class SalesProductComponent implements OnInit {

  constructor(
    private router: Router
  ) { }

  ngOnInit() {
  }
  
  Confirm()
  {
    this.router.navigateByUrl('/app/sale/create');
  }

  close()
  {
    window.history.back();
  }
}
