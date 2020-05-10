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
import { CatalogDesignService } from '../../_service/catalog-design.service';
import { CatalogMastView } from '../../_model/catalog-mast';

@Component({
  selector: 'app-catalog-border-color-search',
  templateUrl: './catalog-border-color-search.component.html',
  styleUrls: ['./catalog-border-color-search.component.scss']
})
export class CatalogBorderColorSearchComponent implements OnInit {

  constructor(
    private _catalogBorderColorSvc: CatalogBorderColorService,
    private _catalgDesignSvc: CatalogDesignService,
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
  public model_design: CatalogMastView = new CatalogMastView();
  //actions: any = {};
  public data: CommonSearchView<CatalogBorderColorView> = new CommonSearchView<CatalogBorderColorView>();
  public validationForm: FormGroup;
  
  public catalogDesignLists: any;
  
  public color: any = [];
  public user: any;
  public designName : any;

  async ngOnInit() {
    this.buildForm();
    this.user = this._authSvc.getLoginUser();
    this.model_search.catalog_id = this._actRoute.snapshot.params.catalog_id;
    this.catalogDesignLists = await this._ddlSvc.getDdlCatalogDesign();
  
    //console.log(this.model.catalog_id);
    this.color = await this._catalogBorderColorSvc.getColor(this.model_search.catalog_id);

    console.log(this.color);

    if(this.model_search.catalog_id != undefined)
    {
      this.model_design = await this._catalgDesignSvc.getInfo(this.model_search.catalog_id);
    }
    this.designName = this.model_design.dsgn_name;
    

    this.checkedList = [];
    //let newcolor: CatalogEmbColorView = new CatalogEmbColorView();
    for (var i = 0; i < this.color.length; i++) {
      if(this.color[i].border_color_mast_id && this.color[i].isSelected == true)
      {
        this.color[i].user_code = this.user.username;
        this.checkedList.push(this.color[i]);
      }
      
    }
    
  }

  buildForm() {
    this.validationForm = this._formBuilder.group({
      catalog_id: [null, [Validators.required]],
    });
  }

  async search() {
    // this.color = await this._catalogBorderColorSvc.getColor(this.model_search.catalog_id);

    // this.checkedList = [];
    // for (var i = 0; i < this.color.length; i++) {
    //   if(this.color[i].border_color_mast_id && this.color[i].isSelected == true)
    //   {
    //     this.color[i].user_code = this.user.username;
    //     this.checkedList.push(this.color[i]);
    //   }
    // }
    this._router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
    this._router.navigate(["/app/catalog-border/"+this.model_search.catalog_id]));
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
      if(this.color[i].emb_color_mast_id && this.color[i].isSelected == true)
      {
        this.color[i].user_code = this.user.username;
        this.checkedList.push(this.color[i]);
      }
      
    }
    //this.checkedList = JSON.stringify(this.checkedList);

    console.log(this.checkedList);
  }

  async save()
  {
      console.log(this.checkedList);
      if(this.checkedList.length == 0)
      {
        await this._msgSvc.warningPopup("ต้องเลือกข้อมูล");
      }
      else
      {
        await this._catalogBorderColorSvc.updateBorderColor(this.checkedList);
      
        await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
        await this.search();
        //this._router.navigateByUrl('/app/color-font');
      }

  }

  

  close() {
    this._router.navigateByUrl('/app/catalog');
  }


}
