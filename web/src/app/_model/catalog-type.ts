import { AppSetting } from '../_constants/app-setting';

export class CatalogTypeView 
{
    public catalog_type_id: number = undefined;
    public catalog_id: number = undefined;
    public catalog_color_id: number = undefined;
    public pdtype_code: string = "";
    public isBorder : boolean = false;
    public pic_base64: string = "";
    public sort_seq: number = 0;
    public status: string = "";
    public created_by: string = "";
    public created_at: any = null;
    public updated_by: string = "";
    public updated_at :any= null;

   
}

export class CatalogTypeSearchView
{
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public catalog_id: number = undefined;
    
}