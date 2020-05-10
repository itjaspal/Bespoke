import { Component, OnInit } from '@angular/core';
import { CatalogColorService } from '../../_service/catalog-color.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { MessageService } from '../../_service/message.service';
import { CatalogColorView } from '../../_model/catalog-color';

@Component({
  selector: 'app-catalog-color-create',
  templateUrl: './catalog-color-create.component.html',
  styleUrls: ['./catalog-color-create.component.scss']
})
export class CatalogColorCreateComponent implements OnInit {

  constructor(
    private _catalgColorSvc: CatalogColorService,
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService,
    private _formBuilder: FormBuilder,
    private _ddlSvc: DropdownlistService,
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

  async ngOnInit() {
     
    this.buildForm();
    this.user = this._authSvc.getLoginUser();
    this.ProductColorLists = await this._ddlSvc.getDdlProductColor();
    console.log(this.ProductColorLists);
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
    this.model.catalog_id = this._actRoute.snapshot.params.catalog_id;
    this.model.catalog_file_path = this.model.file.name;

    if(this.model.pdcolor_code == "")
    {
      await this._msgSvc.warningPopup("ต้องใส่ข้อมูล");
    }
    else
    {
      await this._catalgColorSvc.create(this.model);

      await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
      this._router.navigateByUrl('/app/catalog-color/'+ this.model.catalog_id); 
    }
    console.log(this.model.file);

  }

  // fileChange(file: File[]) {
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

}
