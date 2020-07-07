import { Component, OnInit, ViewChild } from '@angular/core';
import { SalesService } from '../../_service/sales.service';
import { CatalogDesignService } from '../../_service/catalog-design.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { MessageService } from '../../_service/message.service';
import { SalesTransactionView, SalesAttachView, TransactionItemView } from '../../_model/sales';
import { CatalogMastView } from '../../_model/catalog-mast';
import { SignaturePad } from 'angular2-signaturepad/signature-pad';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { forkJoin, Observable, of } from 'rxjs';
import { startWith, debounceTime, switchMap, map, catchError } from 'rxjs/operators';
import { CustomerView } from '../../_model/customer-view';
import { AddressDBView } from '../../_model/address-dbview';
import { CommonService } from '../../_service/common.service';
import { CustomerService } from '../../_service/customer.service';
import { AnyMxRecord } from 'dns';

@Component({
  selector: 'app-sales-edit',
  templateUrl: './sales-edit.component.html',
  styleUrls: ['./sales-edit.component.scss']
})
export class SalesEditComponent implements OnInit {

  @ViewChild('sign_manager') sign_manager: SignaturePad;
  @ViewChild('sign_customer') sign_customer: SignaturePad;

  public options: Object = { // passed through to szimek/signature_pad constructor

    'minWidth': 3,
    'canvasWidth':  270,
    'canvasHeight': 120
  };


  constructor(
    private _salesSvc: SalesService,
    private _catalgDesignSvc: CatalogDesignService,
    private _actRoute: ActivatedRoute,
    private _router: Router,
    private _authSvc: AuthenticationService,
    private _msgSvc: MessageService,
    private _formBuilder: FormBuilder,
    private _customerSvc: CustomerService,
    private _commonSvc: CommonService
  ) { }

  toHighlight: string = ''; 
  public filteredCustomerByName: Observable<CustomerView>;
  public filteredCustomerByTel: Observable<CustomerView>;
  public filteredAddressByZipCode: Observable<AddressDBView>;
  public filteredAddressBySubDistrict: Observable<AddressDBView>;
  public filteredAddressByDistrict: Observable<AddressDBView>;
  public filteredAddressByProvince: Observable<AddressDBView>;


   

  public model: SalesTransactionView = new SalesTransactionView();
  public model_item: TransactionItemView = new TransactionItemView();
  public model_design: CatalogMastView = new CatalogMastView(); 
  public model_customer: any;
  public saleTransactionId: number = 0;
  public user: any;
  
  public model_attach: SalesAttachView = new SalesAttachView();
  actions: any = {};
  public designName : any;
  datas: any;
  total : any = 0;
  public total_qty:any;
  public total_amt:any;

  public validationForm: FormGroup;
  
  public confirmList:any;
  public embroidery: string="";
  public font_name: number = 0;
  public font_color: number = 0;      
  public add_price: number = 0; 

  public branch_code : string = "";
  public branch_name : string = "";
  

  async ngOnInit() {
    this.buildForm();
    this.setupAutoComplete();
    this.user = this._authSvc.getLoginUser();
    //this.branchName = this.user.branch.branch.branchCode + ' - ' + this.user.branch.branch.branchNameThai;

    this.branch_code = this.user.branch.branch.branchCode;
    this.branch_name = this.user.branch.branch.branchNameThai;
    //this.model_doc_search.branchId = this.user.branch.branchId;

    this.saleTransactionId = this._actRoute.snapshot.params.id;
    this.model = await this._salesSvc.getSalesTransactionInfo(this.saleTransactionId);

   
    console.log(this.model);
    
    this.total = this.model.total_amt + this.model.add_price;
    if(this.model.catalog_id != undefined)
    {
      this.model_design = await this._catalgDesignSvc.getInfo(this.model.catalog_id);
    }
    this.designName = this.model_design.dsgn_name;

    this.datas = await this._salesSvc.getInquiryAttachFile(this.saleTransactionId);
  }

  calculate() {
    this.model.add_price = 0;
    this.model.total_amt = 0;
    this.model.total_qty = 0;
    for (var i = 0; i < this.model.transactionItem.length; i++) {
      
      this.model.total_amt += this.model.transactionItem[i].amt;
      this.model.total_qty += this.model.transactionItem[i].qty;
      this.model.add_price = this.model.transactionItem[i].add_price;
    }

    this.total = this.model.total_amt + this.model.add_price;
  }

  delete(_index) {
    this.model.transactionItem.splice(_index, 1);
    this.calculate();
  }

  openSearchItemModal()
  {

  }
  
  ngAfterViewInit() {
    //this.buildForm();
    // this.signaturePad is now available
    this.sign_manager.set('minWidth', 3); // set szimek/signature_pad options at runtime
    this.sign_manager.clear(); // invoke functions from szimek/signature_pad API

    this.sign_customer.set('minWidth', 3); // set szimek/signature_pad options at runtime
    this.sign_customer.clear(); // invoke functions from szimek/signature_pad API
  }

  
 
  drawComplete_manager() {
    // will be notified of szimek/signature_pad's onEnd event
    //console.log(this.sign_manager.toDataURL());
    this.model.sign_manager = this.sign_manager.toDataURL();
    console.log(this.model.sign_manager);
  }

  drawComplete_customer() {
    // will be notified of szimek/signature_pad's onEnd event
    //console.log(this.manager_sign.toDataURL());
    //console.log(this.sign_customer.toDataURL());
    this.model.sign_customer = this.sign_customer.toDataURL();
    console.log(this.model.sign_customer);
  }
 
  drawStart() {
    // will be notified of szimek/signature_pad's onBegin event
    
    console.log('begin drawing');
    //console.log("Manager : " + this.sign_manager.toDataURL());
    //console.log("Customer : " + this.sign_customer.toDataURL());
  }

  drawStart2() {
    // will be notified of szimek/signature_pad's onBegin event
    
    console.log('begin drawing 2');
    //console.log("Manager : " + this.sign_manager.toDataURL());
    //console.log("Customer : " + this.sign_customer.toDataURL());
  }

  

  buildForm() {
    this.validationForm = this._formBuilder.group({
      doc_date: [null, [Validators.required]],
      req_date: [null, [Validators.required]],  
      ref_no: [null, [Validators.required]],    
      remark: [null, []],
      cust_name: [null, [Validators.required]],
      address1: [null, [Validators.required]],
      subDistrict: [null, [Validators.required]],
      district: [null, [Validators.required]],
      province: [null, [Validators.required]],
      zipCode: [null, [Validators.required]],
      tel: [null, [Validators.required]]
      // sign_manager: [null, []],
      // sign_customer: [null, []],
      
    });
  }

  async Confirm()
  {
    //console.log(this.confirmList);
    
    //this.model.doc_no = this.docNo;
    this.model.total_qty = this.total_qty;
    this.model.total_amt = this.total_amt;
    this.model.embroidery = this.embroidery;
    this.model.font_name = this.font_name;
    this.model.font_color = this.font_color;
    this.model.add_price = this.add_price;
    this.model.user_code = this.user.username;
    this.model.doc_status = "PAL";
    
    this.model.transactionItem = this.confirmList;
    console.log(this.model);
    
    if(this.model.sign_customer == "" || this.model.sign_manager == "")
    {
      await this._msgSvc.warningPopup("ต้องใส่ข้อมูล");
    }
    else
    {
      await this._salesSvc.create(this.model);

      await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
      this._router.navigateByUrl('/app/sale'); 
    }
    //console.log(this.model.file);
  }

  close()
  {
    window.history.back();
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
            let result = this.inquiryCustomer("name", value);
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
