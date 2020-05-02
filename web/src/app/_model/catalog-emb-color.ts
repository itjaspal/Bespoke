import { AppSetting } from '../_constants/app-setting';

export class CatalogEmbColorView 
{
    public catalog_emb_color_id: number = undefined;
    public catalog_id: number = undefined;
    public emb_color_code: string = "";
    public pic_file_path: string = "";
    public pic_base64: string = "";
    public created_by: string = "";
    public created_at: any = null;
    public updated_by: string = "";
    public updated_at :any= null;
    //public isSelected :boolean = false;

}

export class CatalogEmbColorSearchView
{
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public catalog_id: number = undefined;
    
}