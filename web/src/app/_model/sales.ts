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