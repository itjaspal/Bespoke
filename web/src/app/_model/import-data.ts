export class ImportDataView {  
    public type: string = "";    
    public datas: DatasView[] = [];   
    
}  

export class DatasView {  
    public code : string = "";
    public name : string = "";
}

export class ImportProductView {  
    public type: string = "";    
    public product: DatasProductView[] = [];  
    
}  

export class DatasProductView {  
    public prod_code : string = "";
    public prod_name : string = "";
    public uom_code : string = "";
    public bar_code : string = "";
    public entity : string = "";
    public pdgrp_code : string = "";
    public pdbrnd_code : string = "";
    public pdtype_code : string = "";
    public pddsgn_code : string = "";
    public pdsize_code : string = "";
    public pdcolor_code : string = "";
    public pdmisc_code : string = "";
    public pdmodel_code : string = "";
    public pdgrp_desc : string = "";
    public pdbrnd_desc : string = ""; 
    public pdtype_desc : string = "";
    public pddsgn_desc : string = "";
    public pdcolor_desc : string = ""; 
    public pdsize_desc : string = "";
    public pdmisc_desc : string = "";
    public pdmodel_desc : string = "";
    public unit_price : number = 0;
    //public prod_status : string = "";
}
