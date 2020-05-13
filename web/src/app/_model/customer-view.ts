import { AppSetting } from "../_constants/app-setting";

export class CustomerView {
    public customerId: number = 0;
    public name: string = "";
    public tel: string = "";
    public addressName: string = "";
    public subDistrict: string = "";
    public district: string = "";
    public province: string = "";
    public zipCode: string = "";
}

export class CustomerAutoCompleteSearchView
{
    
    public type:string = ""; // tel, name
    public txt:string =""; 
}

export class CustomerSearchView
{
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public name:string = "";
}
