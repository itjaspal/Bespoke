import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../_service/product.service';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { AuthenticationService } from '../../_service/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductSyncSearchView } from '../../_model/product';
import { ImportDataService } from '../../_service/import-data.service';
import { MessageService } from '../../_service/message.service';
import { ImportProductView } from '../../_model/import-data';

@Component({
  selector: 'app-product-sync',
  templateUrl: './product-sync.component.html',
  styleUrls: ['./product-sync.component.scss']
})
export class ProductSyncComponent implements OnInit {

  constructor(
    private _productSvc: ProductService,
    private _ddlSvc: DropdownlistService,
    private _authSvc: AuthenticationService,
    private _actRoute: ActivatedRoute,
    private importSvc: ImportDataService,
    private _msgSvc: MessageService,
    private _router: Router
  ) { }

  public model_search: ProductSyncSearchView = new ProductSyncSearchView();
  public model: ImportProductView = new ImportProductView();
  public ddlProductDesign: any;
  public data : any;

  async ngOnInit() {
    this.ddlProductDesign = await this._ddlSvc.getDdlProductDesign();
  }

  async sync()
  {

    this.data = this._productSvc.syncProduct(this.model_search);



    await this.importSvc.importProduct(this.model);

    await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
    this._router.navigateByUrl('/app/product/sync');
    
  }

}
