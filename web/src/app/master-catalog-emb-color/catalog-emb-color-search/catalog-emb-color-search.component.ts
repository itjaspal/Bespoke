import { Component, OnInit } from '@angular/core';
import { CatalogEmbColorService } from '../../_service/catalog-emb-color.service';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { MessageService } from '../../_service/message.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { DomSanitizer } from '@angular/platform-browser';
import { CommonSearchView } from '../../_model/common-search-view';
import { CatalogEmbColorView, CatalogEmbColorSearchView } from '../../_model/catalog-emb-color';
import { ColorFontService } from '../../_service/color-font.service';

@Component({
  selector: 'app-catalog-emb-color-search',
  templateUrl: './catalog-emb-color-search.component.html',
  styleUrls: ['./catalog-emb-color-search.component.scss']
})
export class CatalogEmbColorSearchComponent implements OnInit {

  masterSelected:boolean = false;
  checklist:any;
  checkedList:any;

  constructor(
    private _catalogEmbColorSvc: CatalogEmbColorService,
    private _ddlSvc: DropdownlistService,
    private _msgSvc: MessageService,
    private _formBuilder: FormBuilder,
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService,
    private sanitizer: DomSanitizer, 
    private _router: Router
    
  ) { }

  public model: CatalogEmbColorView = new CatalogEmbColorView();
  public model_search: CatalogEmbColorSearchView = new CatalogEmbColorSearchView();
  
  //actions: any = {};
  public data: CommonSearchView<CatalogEmbColorView> = new CommonSearchView<CatalogEmbColorView>();
  public validationForm: FormGroup;
  
  public catalogDesignLists: any;
  public color: any = [];
  public selectColor;

  // master_checked: boolean = false;
  // master_indeterminate: boolean = false;
  // checkbox_list = [];
  
  

  async ngOnInit() {
    this.buildForm();
    this.model_search.catalog_id = this._actRoute.snapshot.params.catalog_id;
    this.catalogDesignLists = await this._ddlSvc.getDdlCatalogDesign();
  
    //console.log(this.model.catalog_id);
    this.color = await this._catalogEmbColorSvc.getColor();
    this.data = await this._catalogEmbColorSvc.search(this.model_search);

    //console.log(this.color);
    //console.log(this.data);

    
    this.checkedList = [];
    for (var i = 0; i < this.color.length; i++) {
      for(var j = 0; j < this.data.datas.length; j++){
        if(this.color[i].color_code == this.data.datas[j].emb_color_code )
        {
          this.color.isSelected = true;
        }
        else
        {
          this.color.isSelected = false;
        }
          

        
      }
      this.checkedList.push(this.color[i]);
      
    }

    console.log(this.color.isSelected);
   
    

    //this.catalog_id.nativeElement.value = this.model_search.catalog_id;
    //this.getCheckedItemList();
  }

  buildForm() {
    this.validationForm = this._formBuilder.group({
      catalog_id: [null, [Validators.required]],
    });
  }

  async search() {
    console.log(this.model_search);
    //this.model_search.catalog_id = this.model.catalog_id;
    this.data = await this._catalogEmbColorSvc.search(this.model_search);
    console.log(this.data); 
  }

  async add() {
    this._router.navigateByUrl('/app/catalog-emb/'+this.model_search.catalog_id+'/create');
  }

  async color_search() {
    this._router.navigateByUrl('/app/catalog-color/'+this.model_search.catalog_id);
  }

  async border_search() {
    this._router.navigateByUrl('/app/catalog-border/'+this.model_search.catalog_id);
  }

  async type_search() {
    this._router.navigateByUrl('/app/catalog-type/'+this.model_search.catalog_id);
  }

  async emb_search() {
    this._router.navigateByUrl('/app/catalog-emb/'+this.model_search.catalog_id);
  }



  async delete(color) {
    console.log(color); 

    this._msgSvc.confirmPopup("ยืนยันลบข้อมูล", async result => {
      if (result) {
        let res: any = await this._catalogEmbColorSvc.delete(color);

        this._msgSvc.successPopup(res.message);

        await this.search();
      }
    })

  }

  

  onChangeSelectAll(event) {
    if (event.checked) {
        this.color.forEach(obj => {
            obj.isSelected = true;
        });
    }
    else {
        this.color.forEach(obj => {
            obj.isSelected = false;
        });
    }

    this.getCheckedItemList();
}


// checked(item){
//   if(this.data.datas.indexOf(item) != -1){
//     return true;
//   }
// }

// // when checkbox change, add/remove the item from the array
// onChange(checked, item){
//   if(checked){
//   this.data.datas.push(item);
//   } else {
//     this.data.datas.splice(this.data.datas.indexOf(item), 1)
//   }
// }

  getCheckedItemList(){
    console.log(this.color);
    this.checkedList = [];
    for (var i = 0; i < this.color.length; i++) {
      if(this.color[i].emb_color_mast_id)
      this.checkedList.push(this.color[i]);
    }
    this.checkedList = JSON.stringify(this.checkedList);

    console.log(this.checkedList);
  }

 

  close() {
    this._router.navigateByUrl('/app/catalog');
  }


}
