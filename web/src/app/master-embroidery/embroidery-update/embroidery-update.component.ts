import { Component, OnInit } from '@angular/core';
import { EmbroideryService } from '../../_service/embroidery.service';
import { MessageService } from '../../_service/message.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { DomSanitizer } from '@angular/platform-browser';
import { EmbMastView } from '../../_model/emb-mast';

@Component({
  selector: 'app-embroidery-update',
  templateUrl: './embroidery-update.component.html',
  styleUrls: ['./embroidery-update.component.scss']
})
export class EmbroideryUpdateComponent implements OnInit {

  constructor(
    private _embSvc: EmbroideryService,
    private _activateRoute: ActivatedRoute,
    private _formBuilder: FormBuilder,
    private _authSvc: AuthenticationService,
    private _msgSvc: MessageService,
    private _router: Router
  ) { }

  public model: EmbMastView = new EmbMastView();
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
      this.model = await this._embSvc.getInfo(this.code);
      this.imgURL = this.model.pic_base64;
      console.log(this.model);
    }
  }

  buildForm() {
    this.validationForm = this._formBuilder.group({
      font_name: [null, [Validators.required]],
      unit_price: [null, []]
    });
  }

  close() {
    window.history.back();
  }

  async save() {

    this.model.pic_base64 = this.imgURL;
    this.model.updated_by = this.user.username;

    await this._embSvc.update(this.model);

    await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
    this._router.navigateByUrl('/app/emb-mast');

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
