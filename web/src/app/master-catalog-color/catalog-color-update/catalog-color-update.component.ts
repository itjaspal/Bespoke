import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CatalogColorView } from '../../_model/catalog-color';
import { AuthenticationService } from '../../_service/authentication.service';
import { CatalogColorService } from '../../_service/catalog-color.service';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { MessageService } from '../../_service/message.service';

@Component({
  selector: 'app-catalog-color-update',
  templateUrl: './catalog-color-update.component.html',
  styleUrls: ['./catalog-color-update.component.scss']
})
export class CatalogColorUpdateComponent implements OnInit {

  constructor(
    private _colorSvc: CatalogColorService,
    private _ddlSvc: DropdownlistService,
    private _activateRoute: ActivatedRoute,
    private _formBuilder: FormBuilder,
    private _authSvc: AuthenticationService,
    private _msgSvc: MessageService,
    private _router: Router
  ) { }

  public model: CatalogColorView = new CatalogColorView();
  public validationForm: FormGroup;
  public ProductColorLists: any;
  public user: any;
  imgURL: any;
  selectedFiles: FileList;
  fileName: any;
  public code : number = undefined;
  public catalog_id : number = undefined;


  async ngOnInit() {
    this.buildForm();
    this.code = this._activateRoute.snapshot.params.id;
    this.catalog_id = this._activateRoute.snapshot.params.catalog_id;
    this.ProductColorLists = await this._ddlSvc.getDdlProductColor();
    this.user = this._authSvc.getLoginUser();
    console.log(this.code);
    

    if (this.code != undefined) {
      this.model = await this._colorSvc.getInfo(this.code,this.catalog_id);
      this.imgURL = this.model.pic_base64;
      console.log(this.model);
    }
  }

  buildForm() {
    this.validationForm = this._formBuilder.group({
      pdcolor_code: [null, [Validators.required]],
      
    });
  }

  close() {
    window.history.back();
  }

  async save() {

    this.model.pic_base64 = this.imgURL;
    this.model.created_by = this.user.username;
    this.model.updated_by = this.user.username;
    this.model.catalog_id = this._activateRoute.snapshot.params.catalog_id;
    this.model.catalog_file_path = this.model.file.name;

    console.log(this.model);
    if(this.model.pdcolor_code == "")
    {
      await this._msgSvc.warningPopup("ต้องใส่ข้อมูล");
    }
    else
    {
      await this._colorSvc.update(this.model);

      await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
      this._router.navigateByUrl('/app/catalog-color/'+ this.model.catalog_id); 
    }
    console.log(this.model.file);


  }

  preview(event) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]); // read file as data url

      reader.onload = (event) => { // called once readAsDataURL is complete
        //this.imgURL = event.target.result;
        this.imgURL = reader.result;

        console.log(this.imgURL);
      }

    }
  }

  fileChange(event) {
    this.selectedFiles = event.target.files;
    this.selectedFiles = event.target.files;
    this.fileName = this.selectedFiles[0].name;
    //console.log('selectedFiles: ' + this.fileName );
    if (this.selectedFiles.length > 0) {
      this.model.file = this.selectedFiles[0];
    } else {
      this.model.file = null;
    }
  
   
    console.log(this.model.file);
  }

}
