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
        public status :string = "";
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
        // public embroidery: string="";
        // public font_name: number = 0;
        // public font_color: number = 0;
        // public add_price: number = 0; 
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
    public font_color: number = 0;
    public add_price: number = 0; 
}

