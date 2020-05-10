import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { CatalogColorService } from '../../_service/catalog-color.service';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { MessageService } from '../../_service/message.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { DomSanitizer } from '@angular/platform-browser';
import { CatalogColorView, CatalogColorSearchView } from '../../_model/catalog-color';
import { CommonSearchView } from '../../_model/common-search-view';
import { CatalogDesignService } from '../../_service/catalog-design.service';
import { CatalogMastView } from '../../_model/catalog-mast';

@Component({
  selector: 'app-catalog-color-search',
  templateUrl: './catalog-color-search.component.html',
  styleUrls: ['./catalog-color-search.component.scss']
})
export class CatalogColorSearchComponent implements OnInit {

  @ViewChild('catalog_id') catalog_id: ElementRef;

  constructor(
    private _catalogColorSvc: CatalogColorService,
    private _catalgDesignSvc: CatalogDesignService,
    private _ddlSvc: DropdownlistService,
    private _msgSvc: MessageService,
    private _formBuilder: FormBuilder,
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService,
    private sanitizer: DomSanitizer, 
    private _router: Router
  ) { }

  public model: CatalogColorView = new CatalogColorView();
  public model_search: CatalogColorSearchView = new CatalogColorSearchView();
  public model_design: CatalogMastView = new CatalogMastView();
  //actions: any = {};
  public data: CommonSearchView<CatalogColorView> = new CommonSearchView<CatalogColorView>();
  public validationForm: FormGroup;
  
  public catalogDesignLists: any;
  public designName : any;

  async ngOnInit() {
    this.buildForm();
    this.model_search.catalog_id = this._actRoute.snapshot.params.catalog_id;
    this.catalogDesignLists = await this._ddlSvc.getDdlCatalogDesign();
    
    //console.log(this.model.catalog_id);
   
    this.data = await this._catalogColorSvc.search(this.model_search);

    //this.catalog_id.nativeElement.value = this.model_search.catalog_id;
    if(this.model_search.catalog_id != undefined)
    {
      this.model_design = await this._catalgDesignSvc.getInfo(this.model_search.catalog_id);
    }
    this.designName = this.model_design.dsgn_name;
    console.log(this.model_design);
  }

  buildForm() {
    this.validationForm = this._formBuilder.group({
      catalog_id: [null, [Validators.required]],
    });
  }

  async search() {
    console.log(this.model_search);
  
    // this.data = await this._catalogColorSvc.search(this.model_search);
    // console.log(this.data); 

    //this._router.navigateByUrl('/app/catalog-color/'+this.model_search.catalog_id);
    this._router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
    this._router.navigate(["/app/catalog-color/"+this.model_search.catalog_id]));
  }

  async add() {
    this._router.navigateByUrl('/app/catalog-color/'+this.model_search.catalog_id+'/create');
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

  async color_search() {
    this._router.navigateByUrl('/app/catalog-color/'+this.model_search.catalog_id);
  }



  async delete(color) {
    console.log(color); 

    this._msgSvc.confirmPopup("ยืนยันลบข้อมูล", async result => {
      if (result) {
        let res: any = await this._catalogColorSvc.delete(color);

        this._msgSvc.successPopup(res.message);

        await this.search();
      }
    })

  }

  close() {
    this._router.navigateByUrl('/app/catalog');
  }


}
