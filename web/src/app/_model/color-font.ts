import { AppSetting } from '../_constants/app-setting';

export class ColorFontView 
{
    public emb_color_mast_id: number = undefined;
    public color_code: string = "";
    public color_name: string = "";
    public pic_file_path: string = "";
    public pic_base64: string = "";
    public created_by: string = "";
    public created_at: any = null;
    public updated_by: string = "";
    public updated_at :any= null;
    public file : any = null;
}

export class ColorFontSearchView
{
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public color_code: string ="";
    public color_name: string = "";
}