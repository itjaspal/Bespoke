import { AppSetting } from '../_constants/app-setting';

export class CatalogColorView 
{
    public catalog_color_id: number = undefined;
    public catalog_id: number = undefined;
    public pddsgn_name:string = ""
    public pdcolor_code: string = "";
    public pdcolor_name: string = "";
    public pic_file_path: string = "";
    public pic_base64: string = "";
    public created_by: string = "";
    public created_at: any = null;
    public updated_by: string = "";
    public updated_at :any= null;

}

export class CatalogColorSearchView
{
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public catalog_id: number = undefined;
    
}