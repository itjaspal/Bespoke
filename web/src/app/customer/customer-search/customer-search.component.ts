import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../../_service/customer.service';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { CustomerAutoCompleteSearchView, CustomerView, CustomerSearchView } from '../../_model/customer-view';
import { AppSetting } from '../../_constants/app-setting';
import { CommonSearchView } from '../../_model/common-search-view';
import { PageEvent } from '@angular/material';
import { MenuSearchView } from '../../_model/menu';

@Component({
  selector: 'app-customer-search',
  templateUrl: './customer-search.component.html',
  styleUrls: ['./customer-search.component.scss']
})
export class CustomerSearchComponent implements OnInit {

  constructor(
    private _custSvc: CustomerService,
    private _ddlSvc: DropdownlistService,
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService
  ) { }

  public toppingList: any = [];
  public model: CustomerSearchView = new CustomerSearchView();
  // public ddlStatus: any;
  // public ddlBranch: Dropdownlist[] = [];
  // public ddlDepartment: Dropdownlist[] = [];  
  public pageSizeOptions: number[] = AppSetting.pageSizeOptions;
  public pageEvent: any;
  actions: any = {};

  public data: CommonSearchView<CustomerView> = new CommonSearchView<CustomerView>();


  async ngOnInit() {    
    this.actions = this._authSvc.getActionAuthorization(this._actRoute);

    if (sessionStorage.getItem('session-menu-search') != null) {
      this.model = JSON.parse(sessionStorage.getItem('session-menu-search'));
      this.search();
    }

    

  }

  async ngOnDestroy() {
    this.saveSession();
  }

  async saveSession() {
    sessionStorage.setItem('session-menu-search', JSON.stringify(this.model));
  }

  async search(event: PageEvent = null) {

    if (event != null) {
      this.model.pageIndex = event.pageIndex;
      this.model.itemPerPage = event.pageSize;
    }

    this.data = await this._custSvc.search(this.model);
  }

  delete(row: MenuSearchView) {

  }

}
