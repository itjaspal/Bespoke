import { AppSetting } from "../_constants/app-setting";

export class ProductSearchView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public prod_code: string = "";
    public bar_code: string = "";
    public prod_tname: string = "";
    public pdbrnd_code: string = "";
    public pddsgn_code: string = "";
    public pdtype_code: string = "";
    public pdcolor_code: string = "";
    public pdsize_code: string = "";
    public uom_code: string = "";
    public status: string = "A";
    
    // public productModelText: string = "";
    // public productColorText: string = "";
    // public productSizeText: string = "";
    // public productDesignText: string = "";
    // public isSaleBed: boolean = true;
}

export class ProductView {
    public id: number = undefined;
    public prod_code: string = "";
    public bar_code: string = "";
    public prod_tname: string = "";
    public pdtype_code: string = "";
    public pdbrnd_code: string = "";
    public pddsgn_code: string = "";
    public pdcolor_code: string = "";
    public pdsize_code: string = "";
    public uom_code: string = "";
    public status: string = "";
    public statusText: string = "";
    public pdtype_desc: string = "";
    public pdbrnd_desc: string = "";
    public pddsgn_desc: string = "";
    public pdcolor_desc: string = "";
    public pdsize_desc: string = "";
    public unit_price: number = 0;
       
}

export class ProductSyncSearchView
{
    public pddsgn_code : string = "";
}
