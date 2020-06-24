import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SalesAttachView } from '../../_model/sales';
import { ActivatedRoute, Router } from '@angular/router';
import { SalesService } from '../../_service/sales.service';
import { MessageService } from '../../_service/message.service';

@Component({
  selector: 'app-sales-attach-file',
  templateUrl: './sales-attach-file.component.html',
  styleUrls: ['./sales-attach-file.component.scss']
})
export class SalesAttachFileComponent implements OnInit {

  constructor(
    private _formBuilder: FormBuilder,
    private _actRoute:ActivatedRoute,
    private _salesSvc: SalesService,
    private _msgSvc: MessageService,
    private _router: Router
  ) { }
  
  public model: SalesAttachView = new SalesAttachView();
  public validationForm: FormGroup;
  selectedFiles: FileList;
  fileName: any;

  ngOnInit() {
    this.buildForm();
  }
  buildForm() {
    this.validationForm = this._formBuilder.group({
      // catalog_id: [null, [Validators.required]],
    });
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

  close() {
    window.history.back();
  }

  async save() {

    
    this.model.co_trns_mast_id = this._actRoute.snapshot.params.catalog_id;
    this.model.pic_file_path = this.model.file.name;

    if(this.model.co_trns_mast_id == 0)
    {
      await this._msgSvc.warningPopup("ต้องใส่ข้อมูล");
    }
    else
    {
      await this._salesSvc.postSalesAttach(this.model);

      await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
      this._router.navigateByUrl('/app/sale'); 
    }
    console.log(this.model.file);

  }


}
