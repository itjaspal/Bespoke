import { Component, OnInit } from '@angular/core';
import { MessageService } from '../../_service/message.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { DomSanitizer } from '@angular/platform-browser';
import { CommonSearchView } from '../../_model/common-search-view';
import { EmbMastView, EmbMastSearchView } from '../../_model/emb-mast';
import { EmbroideryService } from '../../_service/embroidery.service';

@Component({
  selector: 'app-embroidery-search',
  templateUrl: './embroidery-search.component.html',
  styleUrls: ['./embroidery-search.component.scss']
})
export class EmbroiderySearchComponent implements OnInit {

  constructor(
    private _embSvc: EmbroideryService,
    private _msgSvc: MessageService,
    private _formBuilder: FormBuilder,
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService,
    private sanitizer: DomSanitizer, 
    private _router: Router
  ) { }

  public model: EmbMastView = new EmbMastView();
  public model_search: EmbMastSearchView = new EmbMastSearchView();
  //actions: any = {};
  public data: CommonSearchView<EmbMastView> = new CommonSearchView<EmbMastView>();

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
      font_name: [null, [Validators.required]],
      unit_price: [null, []],    
    
      
    });
  }

  async search() {
    //console.log(this.model_search);
    this.data = await this._embSvc.search(this.model_search);
    //console.log(this.data);
  }

  async save()
  {
    console.log(this.model);
    this.model.pic_base64 = this.imgURL;
    this.model.created_by = this.user.username;
    this.model.updated_by = this.user.username;

    console.log(this.model.font_name);
    
    if(this.model.font_name == "")
    {
      await this._msgSvc.warningPopup("ต้องใส่ข้อมูล");
    }
    else
    {
      await this._embSvc.create(this.model);
     
      await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
      await this.search();
      //this._router.navigateByUrl('/app/emb-mast');
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
        let res: any = await this._embSvc.delete(color);

        this._msgSvc.successPopup(res.message);

        await this.search();
      }
    })

  }

}
