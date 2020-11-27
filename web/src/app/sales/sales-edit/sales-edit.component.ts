import { SalesProductSearchComponent } from './../sales-product-search/sales-product-search.component';
import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
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
import { MatDialog, MatSnackBar, PageEvent } from '@angular/material';
import { ShareDataService } from '../../_service/share-data.service';

@Component({
  selector: 'app-sales-edit',
  templateUrl: './sales-edit.component.html',
  styleUrls: ['./sales-edit.component.scss']
})
export class SalesEditComponent implements OnInit {

  
  // *********** Set for Signature Pad  ***************//
  // @ViewChild('sign_manager') sign_manager: SignaturePad;
  // @ViewChild('sign_customer') sign_customer: SignaturePad;

  // public options: Object = { // passed through to szimek/signature_pad constructor

  //   'minWidth': 3,
  //   'canvasWidth':  270,
  //   'canvasHeight': 120
  // };
  // *********** End Signature Pad  ***************//



  constructor(
    private _salesSvc: SalesService,
    private _catalgDesignSvc: CatalogDesignService,
    private _actRoute: ActivatedRoute,
    private _router: Router,
    private _dialog: MatDialog,
    private _authSvc: AuthenticationService,
    private _msgSvc: MessageService,
    private _formBuilder: FormBuilder,
    private _customerSvc: CustomerService,
    private _commonSvc: CommonService,
    private _data: ShareDataService,
    private cdr: ChangeDetectorRef
  ) { }



  // ********************* Set for Autocomplete ************************//
  toHighlight: string = ''; 
  public filteredCustomerByName: Observable<CustomerView>;
  public filteredCustomerByTel: Observable<CustomerView>;
  public filteredAddressByZipCode: Observable<AddressDBView>;
  public filteredAddressBySubDistrict: Observable<AddressDBView>;
  public filteredAddressByDistrict: Observable<AddressDBView>;
  public filteredAddressByProvince: Observable<AddressDBView>;

  // ************************** End Autocomplete ************************//



  public model: SalesTransactionView = new SalesTransactionView();
  public model_item: TransactionItemView = new TransactionItemView();
  public model_design: CatalogMastView = new CatalogMastView(); 
  //public model_customer: any;
  public saleTransactionId: number = 0;
  public user: any;
  
  //public model_attach: SalesAttachView = new SalesAttachView();
  actions: any = {};
  public designName : any;
  datas: any;
  total : any = 0;
  public total_qty:any;
  public total_amt:any;
  public catalog_id : any;
  public catalog_color_id : any;

  public validationForm: FormGroup;
  
  public confirmList:any;
  public embroidery: string="";
  public font_name: number = 0;
  public font_color: number = 0;      
  public add_price: number = 0; 

  public branch_code : string = "";
  public branch_name : string = "";
  public type: any = [];
  public color: any = [];
  public emb: any = [];
  public color_font: any = []; 
  public message:any;

  async ngOnInit() {
    this.buildForm();
    this.setupAutoComplete();
    this.user = this._authSvc.getLoginUser();
    this.branch_code = this.user.branch.branch.branchCode;
    this.branch_name = this.user.branch.branch.branchNameThai;
    

    this.saleTransactionId = this._actRoute.snapshot.params.id;
    this.model = await this._salesSvc.getSalesTransactionInfo(this.saleTransactionId);
    console.log(this.model);

    this.catalog_id = this.model.catalog_id;
    this.catalog_color_id = this.model.catalog_color_id;
   
    this.type = await this._salesSvc.getTypeInCatalogColor(this.catalog_id,this.catalog_color_id);
    this.color = await this._salesSvc.getColorInCatalog(this.catalog_id);

    this.emb = await this._salesSvc.getEmbroidery();
    this.color_font = await this._salesSvc.getColorFont(this.catalog_id);

    
    
    this.total = this.model.total_amt + this.model.add_price;
    this.add_price = this.model.add_price;
    if(this.model.catalog_id != undefined)
    {
      this.model_design = await this._catalgDesignSvc.getInfo(this.model.catalog_id);
    }
    this.designName = this.model_design.dsgn_name;

    this.datas = await this._salesSvc.getInquiryAttachFile(this.saleTransactionId);
    
    console.log(this.model);
    this.confirmList = this.model.transactionItem;
  }

  
  calculate() {
    this.model.add_price = 0;
    this.model.total_amt = 0;
    this.model.total_qty = 0;
    for (var i = 0; i < this.model.transactionItem.length; i++) {
      
      this.model.total_amt += this.model.transactionItem[i].amt;
      this.model.total_qty += this.model.transactionItem[i].qty;
      if(this.model.transactionItem[i].add_price != 0)
      {
        this.model.add_price = this.model.transactionItem[i].add_price;
      }
    }

    this.total = this.model.total_amt + this.model.add_price;
  }


  delete(_index) {
    this.model.transactionItem.splice(_index, 1);
    this.confirmList = this.model.transactionItem;
    this.calculate();
  }

  openSearchItem()
  {
    
    this._data.oldSales.subscribe(message => this.message = this.model)
    //console.log(this.message);
    this._data.editSales(this.message)

    
    this._router.navigateByUrl('/app/sale/product-search/'+this.catalog_id+'/'+this.catalog_color_id+'/'+this.model.co_trns_mast_id); 
  }
  
 
  

  // ************************** Function for Signature Pad ***************************** //

  // ngAfterViewInit() {
  //   this.sign_manager.set('minWidth', 3); // set szimek/signature_pad options at runtime
  //   this.sign_manager.clear(); // invoke functions from szimek/signature_pad API

  //   this.sign_customer.set('minWidth', 3); // set szimek/signature_pad options at runtime
  //   this.sign_customer.clear(); // invoke functions from szimek/signature_pad API
  // }

  // drawComplete_manager() {
  //   // will be notified of szimek/signature_pad's onEnd event   
  //   this.model.sign_manager = this.sign_manager.toDataURL();
  //   console.log(this.model.sign_manager);
  // }

  // drawComplete_customer() {
  //   // will be notified of szimek/signature_pad's onEnd event
 
  //   this.model.sign_customer = this.sign_customer.toDataURL();
  //   console.log(this.model.sign_customer);
  // }
 
  // drawStart() {

   
  //   // will be notified of szimek/signature_pad's onBegin event
    
  //   console.log('begin drawing');    
    
 
  // }

  // drawStart2() {
  //   // will be notified of szimek/signature_pad's onBegin event    
  //   console.log('begin drawing 2');
  // }

 // ******************** End of Function for Signature Pad ********************** //
  

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
    // this.model.total_qty = this.total_qty;
    // this.model.total_amt = this.total_amt;
    // this.model.embroidery = this.embroidery;
    // this.model.font_name = this.font_name;
    // this.model.font_color = this.font_color;
    // this.model.add_price = this.add_price;
    // this.model.user_code = this.user.username;
    // this.model.doc_status = "PAL";
    
    this.model.transactionItem = this.confirmList;
    console.log(this.model);
    
    // if(this.model.sign_customer == "" || this.model.sign_manager == "")
    // {
    //   await this._msgSvc.warningPopup("ต้องใส่ข้อมูล");
    // }
    // else
    // {
      this.model.font_color_base64 = "";
      this.model.font_name_base64 = "";

      for(var x = 0;x<this.model.transactionItem.length; x++)
      {
        this.model.transactionItem[x].color_base64 = "";
        this.model.transactionItem[x].font_color_base64 = "";
        this.model.transactionItem[x].font_name_base64 = "";
        this.model.transactionItem[x].type_base64 = "";

      }
      console.log(this.model);
      
      await this._salesSvc.update(this.model);

      await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
      this._router.navigateByUrl('/app/sale'); 
   
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

  //========= End of AutoComplete ================//
}
