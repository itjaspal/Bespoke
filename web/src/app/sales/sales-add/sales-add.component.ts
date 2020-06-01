import { Component, Input, OnInit, OnChanges } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-sales-add',
  templateUrl: './sales-add.component.html',
  styleUrls: ['./sales-add.component.scss']
  //inputs:['checkedList']
})
export class SalesAddComponent implements OnInit {
  @Input() public checkedList;
  
  constructor(
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService,
    private sanitizer: DomSanitizer, 
    private _router: Router
  ) { }

  //public checkedList:any;
  
  ngOnInit() {
     //this.checkedList = this._actRoute.snapshot.params.type;
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
