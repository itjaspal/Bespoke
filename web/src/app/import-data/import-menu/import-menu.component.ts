import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-import-menu',
  templateUrl: './import-menu.component.html',
  styleUrls: ['./import-menu.component.scss']
})
export class ImportMenuComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  close()
  {
    window.history.back();
  }

}
