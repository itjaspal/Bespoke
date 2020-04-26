import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { CatalogDesignService } from '../../_service/catalog-design.service';
import { CatalogMastView } from '../../_model/catalog-mast';

@Component({
  selector: 'app-catalog-design-view',
  templateUrl: './catalog-design-view.component.html',
  styleUrls: ['./catalog-design-view.component.scss']
})
export class CatalogDesignViewComponent implements OnInit {

  constructor(
    private _activateRoute: ActivatedRoute,
    private _formBuilder: FormBuilder,
    private _catalgDesignSvc: CatalogDesignService
  ) { }

  public model: CatalogMastView = new CatalogMastView();
  public code : number = undefined;
   
  async ngOnInit() {
   

    this.code = this._activateRoute.snapshot.params.id;
    //console.log(this.code);
    
     if (this.code != undefined) {
       this.model = await this._catalgDesignSvc.getInfo(this.code);
     }

    
  }

  close() {
    window.history.back();
  }


}
