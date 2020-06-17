import { Component, OnInit ,ViewChild} from '@angular/core';
import { Router } from '@angular/router';
import { ShareDataService } from '../../_service/share-data.service';
import { AuthenticationService } from '../../_service/authentication.service';
import { SalesService } from '../../_service/sales.service';
import { DocNoView, DocNoSearchView, SalesTransactionView } from '../../_model/sales';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomerService } from '../../_service/customer.service';
import { startWith, debounceTime, switchMap, map, catchError } from 'rxjs/operators';
import { forkJoin, Observable, of } from 'rxjs';
import { AddressDBView } from '../../_model/address-dbview';
import { CustomerView } from '../../_model/customer-view';
import { CommonService } from '../../_service/common.service';
import { SignaturePad } from 'angular2-signaturepad/signature-pad';

@Component({
  selector: 'app-sales-summary',
  templateUrl: './sales-summary.component.html',
  styleUrls: ['./sales-summary.component.scss']
})
export class SalesSummaryComponent implements OnInit {

  @ViewChild(SignaturePad) manager_sign: SignaturePad;
  @ViewChild(SignaturePad) customer_sign: SignaturePad;
 
  
  public options: Object = { // passed through to szimek/signature_pad constructor
    'backgroundColor': 'rgb(222, 224, 226)',
    'minWidth': 5,
    //'canvasWidth': '350',
    // 'canvasHeight': 120
  };

  constructor(
    private _data: ShareDataService,
    private _salesSvc: SalesService,
    private _customerSvc: CustomerService,
    private _formBuilder: FormBuilder,
    private _authSvc: AuthenticationService,
    private _commonSvc: CommonService,
    private router: Router
  ) { }

  toHighlight: string = '';

  public model: SalesTransactionView = new SalesTransactionView();
  


  public filteredCustomerByName: Observable<CustomerView>;
  public filteredCustomerByTel: Observable<CustomerView>;
  public filteredAddressByZipCode: Observable<AddressDBView>;
  public filteredAddressBySubDistrict: Observable<AddressDBView>;
  public filteredAddressByDistrict: Observable<AddressDBView>;
  public filteredAddressByProvince: Observable<AddressDBView>;


  public validationForm: FormGroup;
  public salesList:any;
  public user: any;
  public branchName : any;
  public docNo: any;
  public model_doc_search: DocNoSearchView = new DocNoSearchView();  
  public model_doc: DocNoView = new DocNoView();  
  actions: any = {};
  selectedFiles: FileList;
  fileName: any;
  

  

  async ngOnInit() {
    this.buildForm();
    this.setupAutoComplete();
    this.user = this._authSvc.getLoginUser();
    this.branchName = this.user.branch.branch.branchCode + ' - ' + this.user.branch.branch.branchNameThai;

    this.model_doc_search.branchId = this.user.branch.branchId;
    //console.log(this.model_doc_search);
    
    this.model_doc = await this._salesSvc.searchDocNo(this.model_doc_search);
    //console.log(this.model_doc.doc_no);
    this.docNo = this.model_doc.doc_no;

    
   

    this._data.selectedSales.subscribe(sales => this.salesList = sales)
    console.log(this.salesList);
    for (var i = 0; i < this.salesList.length; i++) {
      for(var j=0;j<this.salesList[i].catalogType.length; j++)
      {
        if(this.salesList[i].catalogType[j].qty > 0)
        {
          
          console.log(this.salesList[i].catalogType[j].catalog_type_id);
        }
      }
      
      
    }
    
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
      tel: [null, [Validators.required]],
      sign_manager: [null, []],
      sign_customer: [null, []],
      
    });
  }

  fileChange(event) {
    this.selectedFiles = event.target.files;
    this.selectedFiles = event.target.files;
    this.fileName = this.selectedFiles[0].name;
    //console.log('selectedFiles: ' + this.fileName );
    if (this.selectedFiles.length > 0) {
      this.model.file = this.selectedFiles[0];
    } else {
      this.model.file = null;
    }
  
   
    console.log(this.model.file);
  }

  ngAfterViewInit() {
    // this.signaturePad is now available
    this.manager_sign.set('minWidth', 5); // set szimek/signature_pad options at runtime
    this.manager_sign.clear(); // invoke functions from szimek/signature_pad API

    this.customer_sign.set('minWidth', 5); // set szimek/signature_pad options at runtime
    this.customer_sign.clear(); // invoke functions from szimek/signature_pad API
  }
 
  drawComplete_manager() {
    // will be notified of szimek/signature_pad's onEnd event
    console.log(this.manager_sign.toDataURL());
    //console.log(this.customer_sign.toDataURL());
  }

  drawComplete_customer() {
    // will be notified of szimek/signature_pad's onEnd event
    //console.log(this.manager_sign.toDataURL());
    console.log(this.customer_sign.toDataURL());
  }
 
  drawStart() {
    // will be notified of szimek/signature_pad's onBegin event
    //console.log('begin drawing');
  }

  

  Confirm()
  {
    this.router.navigateByUrl('/app/sale/summary');
  }

  close()
  {
    window.history.back();
  }

  print()
  {
    // window.open('file:///D:/Angular/Project/Bespoke/web/src/assets/images-prod/tel.pdf');
    window.open('http://192.168.9.50/bespoke/assets/images-prod/order.pdf','_blank');
  }
  points = [];
  signatureImage;

  showImage(data) {
    this.signatureImage = data;
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
