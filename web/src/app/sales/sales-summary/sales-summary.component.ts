import { Component, OnInit ,ViewChild, ViewChildren, QueryList, ElementRef} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
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
import { CatalogDesignService } from '../../_service/catalog-design.service';
import { CatalogMastView } from '../../_model/catalog-mast';
import { MessageService } from '../../_service/message.service';

@Component({
  selector: 'app-sales-summary',
  templateUrl: './sales-summary.component.html',
  styleUrls: ['./sales-summary.component.scss']
})
export class SalesSummaryComponent implements OnInit {

  //  @ViewChild(SignaturePad) sign_manager: SignaturePad;
  //  @ViewChild(SignaturePad) sign_customer: SignaturePad;
   @ViewChild('sign_manager') sign_manager: SignaturePad;
   @ViewChild('sign_customer') sign_customer: SignaturePad;
 
  
 
 
  
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
    private _catalgDesignSvc: CatalogDesignService,
    private _customerSvc: CustomerService,
    private _formBuilder: FormBuilder,
    private _authSvc: AuthenticationService,
    private _actRoute:ActivatedRoute,
    private _commonSvc: CommonService,
    private _msgSvc: MessageService,
    private _router: Router
  ) { }

  toHighlight: string = '';

  public model: SalesTransactionView = new SalesTransactionView();
  public model_item: TransactionItemView = new TransactionItemView();
  public model_design: CatalogMastView = new CatalogMastView(); 


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
  public fileName: any;
  public confirmList:any;
  sizeSelected:any;
  typeSelected:any;
  public total_qty:any;
  public total_amt:any;
  public designName : any;
  public catalog_id : any;
  

  catalog_size_id:any;
  pdsize_code:any;
  pdsize_name:any;
  prod_code:any;
  prod_tname:any;
  unit_price:any;

  public embroidery: string="";
  public font_name: number = 0;
  public font_color: number = 0;      
  public add_price: number = 0; 

  public branch_code : string = "";
  public branch_name : string = "";
  public total : any;

  async ngOnInit() {
    this.buildForm();
    this.setupAutoComplete();
    this.user = this._authSvc.getLoginUser();
    this.branchName = this.user.branch.branch.branchCode + ' - ' + this.user.branch.branch.branchNameThai;

    this.branch_code = this.user.branch.branch.branchCode;
    this.branch_name = this.user.branch.branch.branchNameThai;
    this.model_doc_search.branchId = this.user.branch.branchId;
    
    this.model_doc = await this._salesSvc.searchDocNo(this.model_doc_search);
    this.docNo = this.model_doc.doc_no;
    this.catalog_id = this._actRoute.snapshot.params.catalog;
    if(this.catalog_id != undefined)
    {
      this.model_design = await this._catalgDesignSvc.getInfo(this.catalog_id);
    }
    this.designName = this.model_design.dsgn_name;

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
   

    //Transaction Data

    this.total_qty = 0;
    this.total_amt = 0;
    this.total = 0;

    for (var i = 0; i < this.salesList.length; i++) {
      
                  
      this.catalog_size_id = 0;
      this.pdsize_code ="";
      this.pdsize_name ="";
      this.prod_code = "";
      this.prod_tname= "";
      this.unit_price = 0;


      for (j = 0; j < this.sizeSelected.length; j++)
      { 
         
       
        if(this.salesList[i].catalog_id == this.sizeSelected[j].catalog_id && this.salesList[i].catalog_type_id == this.sizeSelected[j].catalog_type_id)
        {
          
          this.catalog_size_id = this.sizeSelected[j].catalog_size_id;
          this.pdsize_code = this.sizeSelected[j].pdsize_code;
          this.pdsize_name = this.sizeSelected[j].pdsize_name;
          this.prod_code = this.sizeSelected[j].prod_code;
          this.prod_tname = this.sizeSelected[j].prod_tname;
          this.unit_price = this.sizeSelected[j].unit_price;
          
          
        }
      }

      console.log(this.catalog_size_id);
      console.log(this.pdsize_code);
      
      
      
      //console.log(this.salesList[i]);
      for (k = 0; k < this.typeSelected.length;k++)
      {
        this.model_item = new TransactionItemView();
        // console.log('sale catalog id: '+this.salesList[i].catalog_id);
        // console.log('type : '+this.typeSelected[k].catalog_id);

        // console.log('sales catalog type id : '+this.salesList[i].catalog_type_id);
        // console.log('type : '+this.typeSelected[k].catalog_type_id);


        if(this.salesList[i].catalog_id == this.typeSelected[k].catalog_id && this.salesList[i].catalog_type_id == this.typeSelected[k].catalog_type_id)
        {
            
            this.model_item.catalog_pic_id = this.typeSelected[k].catalog_pic_id;
             this.model_item.catalog_type_code = this.typeSelected[k].catalog_type_code;
            this.model_item.type_base64 = this.typeSelected[k].pic_base64;
            this.model_item.qty = this.typeSelected[k].qty;
            //this.total_qty = this.total_qty + this.typeSelected[k].qty;

            this.model_item.catalog_id = this.salesList[i].catalog_id;
            this.model_item.catalog_color_id = this.salesList[i].catalog_color_id;
            this.model_item.catalog_type_id = this.salesList[i].catalog_type_id;
            this.model_item.pdtype_code = this.salesList[i].pdtype_code;
            this.model_item.pdtype_tname = this.salesList[i].pdtype_tname;
            this.model_item.is_border = this.salesList[i].is_border;
            this.model_item.size_sp = this.salesList[i].size_sp;
            this.model_item.color_base64 = this.salesList[i].pic_color;
            
            this.model_item.amt  = this.model_item.qty * this.model_item.unit_price;
            this.model_item.remark = this.salesList[i].remark;

            this.model_item.catalog_size_id = this.catalog_size_id;
            this.model_item.pdsize_code = this.pdsize_code;
            this.model_item.pdsize_name = this.pdsize_name;
            this.model_item.prod_code = this.prod_code;
            this.model_item.prod_tname = this.prod_tname;
            this.model_item.unit_price = this.unit_price;
            this.model_item.amt = this.model_item.unit_price * this.model_item.qty ;
            
            this.total_qty = this.total_qty + this.model_item.qty;
            this.total_amt = this.total_amt + this.model_item.amt;

            this.model.branch_code = this.branch_code;
            this.model.branch_name = this.branch_name;
            this.model.catalog_id = this.salesList[i].catalog_id;

            if(this.model_item.catalog_type_code == 'A')
            {
              this.model_item.embroidery  = "";
              this.model_item.font_name = 0;
              this.model_item.font_name_base64 = "";
              this.model_item.font_color = 0;
              this.model_item.font_color_base64 = "";
              this.model_item.add_price = 0;

              this.embroidery = "";
              this.font_name = 0;
              this.font_color = 0; 
              this.add_price = 0;
              
            }
            else
            {
              this.model_item.embroidery  = this.salesList.embroidery;
              this.model_item.font_name = this.salesList.font_name;
              this.model_item.font_name_base64 = this.salesList.font_name_base64;
              this.model_item.font_color = this.salesList.font_color;
              this.model_item.font_color_base64 = this.salesList.font_color_base64;
              this.model_item.add_price = this.salesList.add_price;

              this.embroidery = this.salesList.embroidery;
              this.font_name = this.salesList.font_name;
              this.font_color = this.salesList.font_color;
              this.add_price = this.salesList.add_price;
              
            }
            this.total = this.total_amt + this.add_price;
          
            console.log(this.typeSelected[k].catalog_type_code);
            console.log(this.model_item.catalog_type_code);

            this.confirmList.push(this.model_item);
            // console.log(this.confirmList);
        }
         
        
      }

      
      
      

      console.log(this.total_qty);
      console.log(this.total_amt);
      console.log(this.confirmList);
      //this.confirmList.push(this.model_item);
      
    }

    
    
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
    
    this.model.doc_no = this.docNo;
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

  // print()
  // {
  //   // window.open('file:///D:/Angular/Project/Bespoke/web/src/assets/images-prod/tel.pdf');
  //   window.open('http://192.168.9.50/bespoke/assets/images-prod/order.pdf','_blank');
  // }
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
