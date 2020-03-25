import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sales-product',
  templateUrl: './sales-product.component.html',
  styleUrls: ['./sales-product.component.scss']
})
export class SalesProductComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }
  
  close()
  {
    window.history.back();
  }
}
