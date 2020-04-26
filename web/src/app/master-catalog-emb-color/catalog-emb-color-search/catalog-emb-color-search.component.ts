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

@Component({
  selector: 'app-catalog-emb-color-search',
  templateUrl: './catalog-emb-color-search.component.html',
  styleUrls: ['./catalog-emb-color-search.component.scss']
})
export class CatalogEmbColorSearchComponent implements OnInit {

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

  async ngOnInit() {
    this.buildForm();
    this.model_search.catalog_id = this._actRoute.snapshot.params.catalog_id;
    this.catalogDesignLists = await this._ddlSvc.getDdlCatalogDesign();
  
    //console.log(this.model.catalog_id);
   
    this.data = await this._catalogEmbColorSvc.search(this.model_search);

    //this.catalog_id.nativeElement.value = this.model_search.catalog_id;
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

  close() {
    this._router.navigateByUrl('/app/catalog');
  }


}
