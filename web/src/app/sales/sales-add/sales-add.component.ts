import { SizeCatalogView } from './../../_model/sales';
import { Component, Input, OnInit, OnChanges } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { DomSanitizer } from '@angular/platform-browser';
import { ShareDataService } from '../../_service/share-data.service';

@Component({
  selector: 'app-sales-add',
  templateUrl: './sales-add.component.html',
  styleUrls: ['./sales-add.component.scss']
  //inputs:['checkedList']
})
export class SalesAddComponent implements OnInit {
  //@Input() public checkedList;
  
  constructor(
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService,
    private sanitizer: DomSanitizer, 
    private _router: Router,
    private _data: ShareDataService
  ) { }

  public checkedList:any;
  //public message:any;
  
  ngOnInit() {
     //this.checkedList = this._actRoute.snapshot.params.type;
     this._data.currentMessage.subscribe(message => this.checkedList = message)
     console.log(this.checkedList);
  }

  Confirm()
  {
    this._router.navigateByUrl('/app/sale/summary');
  }

  close()
  {
    window.history.back();
  }

}
