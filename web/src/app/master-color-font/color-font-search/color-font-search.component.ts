import { Component, OnInit } from '@angular/core';
import { ColorFontView, ColorFontSearchView } from '../../_model/color-font';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { ColorFontService } from '../../_service/color-font.service';
import { PageEvent, MAT_DIALOG_DATA } from '@angular/material';
import { CommonSearchView } from '../../_model/common-search-view';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { MessageService } from '../../_service/message.service';
import { Inject } from '@angular/core';

@Component({
  selector: 'app-color-font-search',
  templateUrl: './color-font-search.component.html',
  styleUrls: ['./color-font-search.component.scss']
})
export class ColorFontSearchComponent implements OnInit {

  constructor(
    private _colorSvc: ColorFontService,
    private _msgSvc: MessageService,
    private _formBuilder: FormBuilder,
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService,
    private sanitizer: DomSanitizer, 
    //@Inject(MAT_DIALOG_DATA) public data: any,
    private _router: Router
  ) { }

  public model: ColorFontView = new ColorFontView();
  public model_search: ColorFontSearchView = new ColorFontSearchView();
  //actions: any = {};
  public data: CommonSearchView<ColorFontView> = new CommonSearchView<ColorFontView>();

  //public imagePath;
  imgURL: any;
  
  //public message: string;
  formGroup: FormGroup;
  public validationForm: FormGroup;
  
  //public imageSource: any;  
  
  public user: any;

  async ngOnInit() {
    this.buildForm();
    this.user = this._authSvc.getLoginUser();
    // if (sessionStorage.getItem('session_colorfont') != null) {
    //   this.model = JSON.parse(sessionStorage.getItem('session_colorfont-search'));
    //   this.search();
    // }
    this.search();
    
  }

 
  buildForm() {
    
    this.validationForm = this._formBuilder.group({
      color_code: [null, [Validators.required]],
      color_name: [null, [Validators.required]],    
    
      
    });
  }

  async search() {
    //console.log(this.model_search);
    this.data = await this._colorSvc.search(this.model_search);
    console.log(this.data);
  }

  async save()
  {
    console.log(this.model);
    this.model.pic_base64 = this.imgURL;
    this.model.created_by = this.user.username;
    this.model.updated_by = this.user.username;

    if(this.model.color_code == "" || this.model.color_name == "")
    {
      await this._msgSvc.warningPopup("ต้องใส่ข้อมูล");
    }
    else
    {
      await this._colorSvc.create(this.model);
     
      await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
      await this.search();
      //this._router.navigateByUrl('/app/color-font');
    }

    
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


  async delete(color) {

    this._msgSvc.confirmPopup("ยืนยันลบข้อมูล", async result => {
      if (result) {
        let res: any = await this._colorSvc.delete(color);

        this._msgSvc.successPopup(res.message);

        await this.search();
      }
    })

  }

}
