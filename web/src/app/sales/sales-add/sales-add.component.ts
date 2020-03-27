import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sales-add',
  templateUrl: './sales-add.component.html',
  styleUrls: ['./sales-add.component.scss']
})
export class SalesAddComponent implements OnInit {

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

}
