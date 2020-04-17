import { Component, OnInit } from '@angular/core';
import { ColorFontService } from '../../_service/color-font.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from '../../_service/message.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ColorFontView } from '../../_model/color-font';
import { AuthenticationService } from '../../_service/authentication.service';

@Component({
  selector: 'app-color-font-update',
  templateUrl: './color-font-update.component.html',
  styleUrls: ['./color-font-update.component.scss']
})
export class ColorFontUpdateComponent implements OnInit {

  constructor(
    private _colorSvc: ColorFontService,
    private _activateRoute: ActivatedRoute,
    private _formBuilder: FormBuilder,
    private _authSvc: AuthenticationService,
    private _msgSvc: MessageService,
    private _router: Router
  ) { }

  public model: ColorFontView = new ColorFontView();
  formGroup: FormGroup;
  public validationForm: FormGroup;
  public code : number = undefined;
  public user: any;

  imgURL: any;

  async ngOnInit() {
   
    this.code = this._activateRoute.snapshot.params.id;
    this.user = this._authSvc.getLoginUser();
    console.log(this.code);
    this.buildForm();

    if (this.code != undefined) {
      this.model = await this._colorSvc.getInfo(this.code);
      this.imgURL = this.model.pic_base64;
      console.log(this.model);
    }
  }

  buildForm() {
    this.validationForm = this._formBuilder.group({
      color_code: [null, [Validators.required]],
      color_name: [null, [Validators.required]]
    });
  }

  close() {
    window.history.back();
  }

  async save() {

    this.model.pic_base64 = this.imgURL;
    this.model.updated_by = this.user.username;

    await this._colorSvc.update(this.model);

    await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
    this._router.navigateByUrl('/app/color-font');

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
