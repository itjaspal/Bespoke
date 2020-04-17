import { AppSetting } from '../_constants/app-setting';

export class EmbMastView
    {
        public emb_mast_id: number = undefined;
        public font_name:string = "";
        public pic_file_path:string = "";
        public pic_base64:string = "";
        public unit_price:number = 0;
        public created_by: string = "";
        public created_at: any = null;
        public updated_by: string = "";
        public updated_at :any= null;

    }

    export class EmbMastSearchView
    {
        public pageIndex: number = 1;
        public itemPerPage: number = AppSetting.itemPerPage;
        public font_name: string = "";
    }