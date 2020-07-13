import { AppSetting } from "../_constants/app-setting";

export class SalesView
{
    public co_trns_mast_id:number = undefined;
    public doc_no :string = "";
    public invoice_no :string = "";
    public doc_date :any = null;
    public req_date :any =null;
    public cust_name :string = "";
    public tot_amt :number = 0;
    public status :string = "";
    public order_status :string = "";
    //public status: string[] = [];
}

export class SalesSearchView
{
        public pageIndex: number = 1;
        public itemPerPage: number = AppSetting.itemPerPage;
        public entity_code:string = "";
        public doc_no:string = "";
        public invoice_no :string = "";
        public to_doc_date :any = null;
        public from_doc_date :any = null;
        //public status :string = "";
        public status: string[] = [];
}


export class SalesTransactionView
{
        public co_trns_mast_id: number = 0;
        public embroidery: string="";
        public font_name: number = 0;
        public font_name_base64: string = ""
        public font_color: number = 0;
        public font_color_base64: string = ""
        public add_price: number = 0; 
        public total_qty: number = 0;
        public total_amt: number = 0;
        public sign_customer:string ="";
        public sign_manager:string = "";
        public branch_code: string = "";
        public branch_name: string = "";
        public doc_no: string = "";
        public doc_date: any = new Date();
        public req_date: any = null;
        public ref_no: string = "";
        public customerId : number = 0;
        public cust_name: string = "";
        public address1: string = "";
        public district: string = "";
        public subDistrict: string = "";
        public province: string = undefined;
        public zipCode: string = "";
        public tel: string = "";
        public remark: string = "";
        public user_code:string = "";
        public doc_status:string ="";
        public file:any = null;
        public catalog_id : number = 0;
        public catalog_color_id : number = 0;
        public transactionItem: TransactionItemView[] = []
}
export class TransactionItemView
{
        public co_trns_det_id : number = 0;
        public catalog_id : number = 0;
        public catalog_color_id: number = 0;
        public catalog_type_id: number = 0;
        public catalog_pic_id: number = 0;
        public catalog_size_id: number = 0;
        public pdtype_code:string = "";
        public pdtype_tname:string = "";
        public is_border:boolean = false;
        public catalog_type_code: string = "";
        public type_base64:string = "";
        public pdsize_code:string ="";
        public pdsize_name: string = "";
        public size_sp: string = "";
        public color_base64: string = "";
        public embroidery : string = "";
        public font_name: number = 0;
        public font_name_base64: string = "";
        public font_color: number = 0;
        public font_color_base64: string = "";
        public add_price: number = 0; 
        public prod_code: string = "";
        public prod_tname: string = "";
        public qty : number = 0;
        public unit_price : number = 0;
        public amt : number = 0;
        public remark : string = "";
        
}

export class SalesSelectTypeView
{
        public catalog_type_id: number = undefined;
        public catalog_id: number = undefined;
        public catalog_color_id: number = undefined;
        public catalog_pic_id : number = undefined;
        public pdtype_code:string = "";
        public pdtype_tname:string = "";
        public is_border:boolean = false;
        public sort_seq:number = 0; 
        public size_sp: string = "";
        public remark:string = "";
        public pic_type:string = "";
        public pic_color:string = "";
        public catalogType: TypeCatalogView[] = [];
        public catalogSize: SizeCatalogView[] = [];
        
}

export class TypeCatalogView
{
        public catalog_pic_id: number = undefined;
        public catalog_type_id: number = undefined;
        public catalog_id: number = undefined;
        public catalog_type_code: string = "";
        public pic_base64: string = "";
        public qty : number = 0;
     
}

export class SizeCatalogView
{
        public catalog_size_id: number = undefined;
        public catalog_id: number = undefined;
        public catalog_type_id: number = undefined;
        public sort_seq: number = 0;
        public pdsize_code: string = "";
        public pdsize_name: string = "";
        
        public prod_code: string;
        public prod_tname: string;
        public unit_price: number = 0;
        public isSelected: boolean = false; 
}

export class FontSelectedView
{
    public embroidery: string="";
    public font_name: number = 0;
    public font_name_base64: string = "";
    public font_color: number = 0;
    public font_color_base64: string = "";
    public add_price: number = 0; 
    public tot_qty:number = 0;
    public tot_amt:number = 0;
    public remark:string="";
}

export class DocNoSearchView
{
        public branchId:number =0;
        public doc_code:string = AppSetting.doc_code;
}
export class DocNoView
{
        public doc_no:string ="";
}

export class ConfirmDataView{
        public catalog_id : number = 0;
        public catalog_color_id: number = 0;
        public catalog_type_id: number = 0;
        public catalog_pic_id: number = 0;
        public catalog_size_id: number = 0;
        public pdtype_code:string = "";
        public pdtype_tname:string = "";
        public is_border:boolean = false;
        public catalog_type_code: string = "";
        public type_base64:string = "";
        public pdsize_code:string ="";
        public pdsize_name: string = "";
        public size_sp: string = "";
        public color_base64: string = "";
        public embroidery : string = "";
        public font_name: number = 0;
        public font_name_base64: string = "";
        public font_color: number = 0;
        public font_color_base64: string = "";
        public add_price: number = 0; 
        public qty : number = 0;
        public unit_price : number = 0;
        public amt : number = 0;
        public remark : string = "";
        
}

export class SalesAttachView
{
        public co_trns_att_file_id : number = 0;
        public co_trns_mast_id : number = 0;
        public pic_file_path: string = "";
        public pic_base64: string = "";
        public fullPath: string = "";
        public file:any = null;
}

export class SalesProductSearchView
{
        public catalog_id : number = 0;
        public catalog_color_id : number = 0;
        public design_name : string = "";
        public type : any;
        public color : any;
        public emb : any;
        public color_font : any;
}

export class SalesProductSelectedView
{
        public catalog_id : number = 0;
        public catalog_color_id : number = 0;
        public design_name : string = "";
        public checkedList : any;
        public emb : any;
        public color : any;
}

export class SalesTransactionUpdateStatusView
{
        public co_trns_mast_id : number = 0;
        public userId : string = "";
}
