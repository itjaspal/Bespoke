import { AppSetting } from '../_constants/app-setting';

export class CatalogSizeView 
{
    public catalog_size_id: number = undefined;
    public catalog_id: number = undefined;
    public catalog_type_id: number = undefined;
    public pdtype_code: string = "";
    public sort_seq: number = 0;
    public pdsize_code: string = "";
    public pdsize_name: string = "";
    public created_by: string = "";
    public created_at: any = null;
    public updated_by: string = "";
    public updated_at :any= null;  
} 

export class CatalogSizeSearchView
{
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public catalog_id: number = undefined;
    public type_id: number = undefined;
    
}