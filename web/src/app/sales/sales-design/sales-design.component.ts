import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sales-design',
  templateUrl: './sales-design.component.html',
  styleUrls: ['./sales-design.component.scss']
})
export class SalesDesignComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }


  close()
  {
    window.history.back();
  }
}
