import { Component, OnInit ,ViewChild} from '@angular/core';
import { Router } from '@angular/router';
import { ShareDataService } from '../../_service/share-data.service';
import { AuthenticationService } from '../../_service/authentication.service';
import { SalesService } from '../../_service/sales.service';
import { DocNoView, DocNoSearchView, SalesTransactionView, TransactionItemView } from '../../_model/sales';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomerService } from '../../_service/customer.service';
import { startWith, debounceTime, switchMap, map, catchError } from 'rxjs/operators';
import { forkJoin, Observable, of } from 'rxjs';
import { AddressDBView } from '../../_model/address-dbview';
import { CustomerView } from '../../_model/customer-view';
import { CommonService } from '../../_service/common.service';
import { SignaturePad } from 'angular2-signaturepad/signature-pad';
import { analyzeAndValidateNgModules } from '@angular/compiler';

@Component({
  selector: 'app-sales-summary',
  templateUrl: './sales-summary.component.html',
  styleUrls: ['./sales-summary.component.scss']
})
export class SalesSummaryComponent implements OnInit {

  @ViewChild(SignaturePad) sign_manager: SignaturePad;
  @ViewChild(SignaturePad) sign_customer: SignaturePad;
 
  
  public options: Object = { // passed through to szimek/signature_pad constructor
    //'backgroundColor': 'rgb(222, 224, 226)',
    //'backgroundColor': 'rgb(255, 255, 255)',
    'minWidth': 3,
    'canvasWidth':  270,
    'canvasHeight': 120
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
  public model_item: TransactionItemView = new TransactionItemView();
  


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
  confirmList:any;
  sizeSelected:any;
  typeSelected:any;
  total_qty:any;
  total_amt:any;

  

  async ngOnInit() {
    this.buildForm();
    this.setupAutoComplete();
    this.user = this._authSvc.getLoginUser();
    this.branchName = this.user.branch.branch.branchCode + ' - ' + this.user.branch.branch.branchNameThai;

    this.model_doc_search.branchId = this.user.branch.branchId;
    
    this.model_doc = await this._salesSvc.searchDocNo(this.model_doc_search);
    this.docNo = this.model_doc.doc_no;

    this._data.selectedSales.subscribe(sales => this.salesList = sales)
    console.log(this.salesList);

    this.confirmList=[];
    this.sizeSelected=[];
    this.typeSelected=[];
    this.total_qty = 0;
    this.total_amt = 0;
    
    
    for (var i = 0; i < this.salesList.length; i++) {
      for(var k = 0;k < this.salesList[i].catalogSize.length; k++)
      {
        if(this.salesList[i].catalogSize[k].isSelected == true)
        {
          this.sizeSelected.push(this.salesList[i].catalogSize[k]);
        }
        //console.log(this.sizeSelected);
      }
      
      for(var j = 0;j < this.salesList[i].catalogType.length; j++)
      {
        //console.log(this.salesList[i].catalogType[j].catalog_pic_id);
        if(this.salesList[i].catalogType[j].qty > 0)
        {
          this.typeSelected.push(this.salesList[i].catalogType[j]);
          
        }
        
       
        
      }
     
      
    }
    // console.log(this.sizeSelected);
    //console.log(this.typeSelected);
    

    for (var i = 0; i < this.salesList.length; i++) {
      
      this.model_item = new TransactionItemView();
      // this.model_item.catalog_id = this.salesList[i].catalog_id;
      // this.model_item.catalog_color_id = this.salesList[i].catalog_color_id;
      // this.model_item.catalog_type_id = this.salesList[i].catalog_type_id;
      // this.model_item.pdtype_code = this.salesList[i].pdtype_code;
      // this.model_item.pdtype_tname = this.salesList[i].pdtype_tname;
      // this.model_item.is_border = this.salesList[i].is_border;
      // this.model_item.size_sp = this.salesList[i].size_sp;
      // this.model_item.color_base64 = this.salesList[i].pic_color;
      // this.model_item.remark = this.salesList[i].remark;
      
      // this.model_item.amt  = this.model_item.qty * this.model_item.unit_price;
      

            

      
      
      for (j = 0; j < this.sizeSelected.length; j++)
      { 
         
        // console.log('j : '+j);
        // console.log('size : ' + this.salesList[i].catalog_size_id);
        // console.log('type : '+ this.sizeSelected[j].catalog_type_id); 
        if(this.salesList[i].catalog_id == this.sizeSelected[j].catalog_id && this.salesList[i].catalog_type_id == this.sizeSelected[j].catalog_type_id)
        {
          console.log(this.sizeSelected[j]);
          this.model_item.catalog_size_id = this.sizeSelected[j].catalog_size_id;
          this.model_item.pdsize_code = this.sizeSelected[j].pdsize_code;
          this.model_item.pdsize_name = this.sizeSelected[j].pdsize_name;
          this.model_item.prod_code = this.sizeSelected[j].prod_code;
          this.model_item.prod_tname = this.sizeSelected[j].prod_tname;
          this.model_item.unit_price = this.sizeSelected[j].unit_price;
          
        }
      }
      
      console.log(this.salesList[i]);
      for (k = 0; k < this.typeSelected.length;k++)
      {
        if(this.salesList[i].catalog_id == this.typeSelected[k].catalog_id && this.salesList[i].catalog_type_id == this.typeSelected[k].catalog_type_id)
        {
            console.log(this.typeSelected[k]);
            this.confirmList.push(this.model_item);  
            
        }
        
        // if(this.salesList[i].catalog_id == this.typeSelected[k].catalog_id && this.salesList[i].catalog_type_id == this.typeSelected[k].catalog_type_id)
        // {
        //   this.model_item.catalog_pic_id = this.typeSelected[k].catalog_id;
        //   this.model_item.catalog_type_code = this.typeSelected[k].catalog_type_code;
        //   this.model_item.type_base64 = this.typeSelected[k].pic_base64;
        //   this.model_item.qty = this.typeSelected[k].qty;
        //   this.total_qty = this.total_qty + this.typeSelected[k].qty;

        //   this.model_item.catalog_id = this.salesList[i].catalog_id;
        //   this.model_item.catalog_color_id = this.salesList[i].catalog_color_id;
        //   this.model_item.catalog_type_id = this.salesList[i].catalog_type_id;
        //   this.model_item.pdtype_code = this.salesList[i].pdtype_code;
        //   this.model_item.pdtype_tname = this.salesList[i].pdtype_tname;
        //   this.model_item.is_border = this.salesList[i].is_border;
        //   this.model_item.size_sp = this.salesList[i].size_sp;
        //   this.model_item.color_base64 = this.salesList[i].pic_color;
          
        //   this.model_item.amt  = this.model_item.qty * this.model_item.unit_price;
        //   this.model_item.remark = this.salesList[i].remark;


        //   if(this.model_item.catalog_type_code == 'A')
        //   {
        //     this.model_item.embroidery  = "";
        //     this.model_item.font_name = 0;
        //     this.model_item.font_name_base64 = "";
        //     this.model_item.font_color = 0;
        //     this.model_item.font_color_base64 = "";
        //     this.model_item.add_price = 0;
        //   }
        //   else
        //   {
        //     this.model_item.embroidery  = this.salesList.embroidery;
        //     this.model_item.font_name = this.salesList.font_name;
        //     this.model_item.font_name_base64 = this.salesList.font_name_base64;
        //     this.model_item.font_color = this.salesList.font_color;
        //     this.model_item.font_color_base64 = this.salesList.font_color_base64;
        //     this.model_item.add_price = this.salesList.add_price;
        //   }
        //   this.confirmList.push(this.model_item);  
        // }
        
        
        //this.confirmList.push(this.model_item);
      }

      
      
      

      this.total_amt = this.total_amt + this.model_item.amt;
      //console.log(this.model_item);
      //this.confirmList.push(this.model_item);
      
    }

    console.log(this.confirmList);
    
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
    //console.log('begin drawing');
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
      //sign_customer: [null, []],
      
    });
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
  // points = [];
  // signatureImage;

  // showImage(data) {
  //   this.signatureImage = data;
  // }


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
