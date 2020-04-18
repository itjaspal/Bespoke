import { AppSetting } from '../_constants/app-setting';

export class CatalogMastView 
{
    public catalog_id: number = undefined;
    public pdbrnd_code: string = "";
    public pddsgn_code: string = "";
    public dsgn_name: string = "";
    public dsgn_desc: string ="";
    public pic_file_path : any = null;
    public pic_base64: string = "";
    public status: string = "";
    public created_by: string = "";
    public created_at: any = null;
    public updated_by: string = "";
    public updated_at :any= null;
}

export class CatalogMastSearchView
{
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public pddsgn_code: string ="";
    public dsgn_name: string ="";
    public dsgn_desc: string ="";
}