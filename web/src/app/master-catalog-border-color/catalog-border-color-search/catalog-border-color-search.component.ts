import { Component, OnInit } from '@angular/core';
import { CatalogBorderColorService } from '../../_service/catalog-border-color.service';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { MessageService } from '../../_service/message.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { DomSanitizer } from '@angular/platform-browser';
import { CatalogBorderColorView, CatalogBorderColorSearchView } from '../../_model/catalog-border-color';
import { CommonSearchView } from '../../_model/common-search-view';

@Component({
  selector: 'app-catalog-border-color-search',
  templateUrl: './catalog-border-color-search.component.html',
  styleUrls: ['./catalog-border-color-search.component.scss']
})
export class CatalogBorderColorSearchComponent implements OnInit {

  constructor(
    private _catalogBorderColorSvc: CatalogBorderColorService,
    private _ddlSvc: DropdownlistService,
    private _msgSvc: MessageService,
    private _formBuilder: FormBuilder,
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService,
    private sanitizer: DomSanitizer, 
    private _router: Router
  ) { }

  masterSelected:boolean = false;
  checklist:any;
  checkedList:any;

  public model: CatalogBorderColorView = new CatalogBorderColorView();
  public model_search: CatalogBorderColorSearchView = new CatalogBorderColorSearchView();
  
  //actions: any = {};
  public data: CommonSearchView<CatalogBorderColorView> = new CommonSearchView<CatalogBorderColorView>();
  public validationForm: FormGroup;
  
  public catalogDesignLists: any;
  
  public color: any = [];
  //public selectColor;

  async ngOnInit() {
    this.buildForm();
    this.model_search.catalog_id = this._actRoute.snapshot.params.catalog_id;
    this.catalogDesignLists = await this._ddlSvc.getDdlCatalogDesign();
  
    //console.log(this.model.catalog_id);
    this.color = await this._catalogBorderColorSvc.getColor();
    //this.data = await this._catalogBorderColorSvc.search(this.model_search);

    //this.catalog_id.nativeElement.value = this.model_search.catalog_id;

    this.checkedList = [];
    for (var i = 0; i < this.color.length; i++) {
      if(this.color[i].emb_color_mast_id)
      this.checkedList.push(this.color[i]);
    }
    this.checkedList = JSON.stringify(this.checkedList);
    console.log(this.checkedList);
    
  }

  buildForm() {
    this.validationForm = this._formBuilder.group({
      catalog_id: [null, [Validators.required]],
    });
  }

  async search() {
    console.log(this.model_search);
    //this.model_search.catalog_id = this.model.catalog_id;
    this.data = await this._catalogBorderColorSvc.search(this.model_search);
    console.log(this.data); 
  }

  async add() {
    this._router.navigateByUrl('/app/catalog-border/'+this.model_search.catalog_id+'/create');
  }

  async color_search() {
    this._router.navigateByUrl('/app/catalog-color/'+this.model_search.catalog_id);
  }

  async emb_search() {
    this._router.navigateByUrl('/app/catalog-emb/'+this.model_search.catalog_id);
  }

  async border_search() {
    this._router.navigateByUrl('/app/catalog-border/'+this.model_search.catalog_id);
  }

  async type_search() {
    this._router.navigateByUrl('/app/catalog-type/'+this.model_search.catalog_id);
  }



  async delete(color) {
    console.log(color); 

    this._msgSvc.confirmPopup("ยืนยันลบข้อมูล", async result => {
      if (result) {
        let res: any = await this._catalogBorderColorSvc.delete(color);

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
