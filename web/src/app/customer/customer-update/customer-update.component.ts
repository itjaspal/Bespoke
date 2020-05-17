import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../../_service/customer.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../../_service/authentication.service';
import { MessageService } from '../../_service/message.service';
import { CustomerView } from '../../_model/customer-view';
import { forkJoin, Observable, of } from 'rxjs';
import { AddressDBView } from '../../_model/address-dbview';
import { startWith, debounceTime, switchMap, map, catchError } from 'rxjs/operators';
import { CommonService } from '../../_service/common.service';


@Component({
  selector: 'app-customer-update',
  templateUrl: './customer-update.component.html',
  styleUrls: ['./customer-update.component.scss']
})
export class CustomerUpdateComponent implements OnInit {

  constructor(
    private _customerSvc: CustomerService,
    private _activateRoute: ActivatedRoute,
    private _formBuilder: FormBuilder,
    private _authSvc: AuthenticationService,
    private _msgSvc: MessageService,
    private _fb: FormBuilder,
    private _commonSvc: CommonService,
    private _router: Router
  ) { }

  public validationForm: FormGroup;
  public user: any;
  public code : number = undefined;

  toHighlight: string = '';

  public model: CustomerView = new CustomerView();
  


  public filteredCustomerByName: Observable<CustomerView>;
  public filteredCustomerByTel: Observable<CustomerView>;
  public filteredAddressByZipCode: Observable<AddressDBView>;
  public filteredAddressBySubDistrict: Observable<AddressDBView>;
  public filteredAddressByDistrict: Observable<AddressDBView>;
  public filteredAddressByProvince: Observable<AddressDBView>;


  async ngOnInit() {
    this.buildForm();
    this.user = this._authSvc.getLoginUser();
    this.code = this._activateRoute.snapshot.params.id;

    if (this.code != undefined) {
      this.model = await this._customerSvc.getInfo(this.code);
     
      console.log(this.model);
    }

    this.setupAutoComplete();
    //console.log(this.filteredCustomerByName);
  }

  private buildForm() {
    this.validationForm = this._fb.group({
      cust_name: [null, [Validators.required]],
      address1: [null, [Validators.required]],
      subDistrict: [null, [Validators.required]],
      district: [null, [Validators.required]],
      province: [null, [Validators.required]],
      zipCode: [null, [Validators.required]],
      tel: [null, [Validators.required]]
    });
  }

  close() {
    window.history.back();
  }



  async save() {
    // let res = await this._saleSvc.postCreateSaleTransaction(this.model);

    // await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
    // this._router.navigateByUrl('/app/sale/bed/view/' + res);
    if(this.model.cust_name == "")
    {
      await this._msgSvc.warningPopup("ต้องใส่ข้อมูล");
    }
    else
    {
      await this._customerSvc.update(this.model);

      await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
      this._router.navigateByUrl('/app/customer'); 
    }

  }

 
  //========= AutoComplete ================//
  setupAutoComplete() {


    //#region  autoComplete Customer
    this.filteredCustomerByName = this.validationForm.controls["cust_name"].valueChanges
      .pipe(
        startWith(''),
        debounceTime(300),

        switchMap(value => {
          if (value !== '' && value != null) {
            if (value.length <= 2) return [];

            //inquiry from service
            let result = this.inquiryCustomer("cust_name", value);
            return (result) ? result : [];
          } else {
            // if no value is present, return null
            return of([]);
          }
        })
      );

    this.filteredCustomerByTel = this.validationForm.controls["tel"].valueChanges
      .pipe(
        startWith(''),
        debounceTime(300),

        switchMap(value => {
          if (value !== '' && value != null) {
            if (value.length <= 2) return [];

            //inquiry from service
            let result = this.inquiryCustomer("tel", value);
            return (result) ? result : [];
          } else {
            // if no value is present, return null
            return of([]);
          }
        })
      );
    //#endregion

    //#region autoComplete address

    this.filteredAddressBySubDistrict = this.validationForm.controls["subDistrict"].valueChanges
      .pipe(
        startWith(''),
        debounceTime(300),

        switchMap(value => {
          if (value !== '' && value != null) {

            //inquiry from service
            let result = this.inquiryAddress("subDistrict", value);
            return (result) ? result : [];
          } else {
            // if no value is present, return null
            return of([]);
          }
        })
      );

    this.filteredAddressByDistrict = this.validationForm.controls["district"].valueChanges
      .pipe(
        startWith(''),
        debounceTime(300),

        switchMap(value => {
          if (value !== '' && value != null) {

            //inquiry from service
            let result = this.inquiryAddress("district", value);
            return (result) ? result : [];
          } else {
            // if no value is present, return null
            return of([]);
          }
        })
      );

    this.filteredAddressByProvince = this.validationForm.controls["province"].valueChanges
      .pipe(
        startWith(''),
        debounceTime(300),

        switchMap(value => {
          if (value !== '' && value != null) {

            //inquiry from service
            let result = this.inquiryAddress("province", value);
            return (result) ? result : [];
          } else {
            // if no value is present, return null
            return of([]);
          }
        })
      );

    this.filteredAddressByZipCode = this.validationForm.controls["zipCode"].valueChanges
      .pipe(
        startWith(''),
        debounceTime(300),

        switchMap(value => {
          if (value !== '' && value != null) {

            //inquiry from service
            let result = this.inquiryAddress("zipcode", value);
            return (result) ? result : [];
          } else {
            // if no value is present, return null
            return of([]);
          }
        })
      );

    //#endregion

  }

  inquiryCustomer(_type: string, _txt: string): Observable<CustomerView> {
    this.toHighlight = _txt;
    return this._customerSvc.postInquiryCustomerByText(_type, _txt)
      .pipe(
        map(result => {
          return result;
        }),

        catchError(_ => {
          return of(null);
        })
      )
  }



  customerSelected(_cust: CustomerView) {
    this.model.cust_name = _cust.cust_name;
    this.model.tel = _cust.tel;
    this.model.address1 = _cust.address1;
    this.model.subDistrict = _cust.subDistrict;
    this.model.district = _cust.district;
    this.model.province = _cust.province;
    this.model.zipCode = _cust.zipCode;
  }

  inquiryAddress(_type: string, _txt: string): Observable<AddressDBView> {
    this.toHighlight = _txt;
    return this._commonSvc.postInquiryAddress(_type, _txt)
      .pipe(
        map(result => {
          return result;
        }),

        catchError(_ => {
          return of(null);
        })
      )
  }

  addressSelected(_addr: AddressDBView) {
    this.model.subDistrict = _addr.subDistrict;
    this.model.district = _addr.district;
    this.model.province = _addr.province;
    this.model.zipCode = _addr.zipcode;
  }


}
