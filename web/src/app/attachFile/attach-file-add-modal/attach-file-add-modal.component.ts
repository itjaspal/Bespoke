import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonService } from './../../_service/common.service';
import { DropdownlistService } from './../../_service/dropdownlist.service';
import { MatDialogRef } from '@angular/material';
import { Dropdownlist } from '../../_model/dropdownlist';
import { AttachFileView } from './../../_model/attach-file-view';
import { Component, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';
import { Inject } from '@angular/core';

@Component({
  selector: 'app-attach-file-add-modal',
  templateUrl: './attach-file-add-modal.component.html',
  styleUrls: ['./attach-file-add-modal.component.scss']
})
export class AttachFileAddModalComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<any>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _ddlSvc: DropdownlistService,
    private _commonSvc: CommonService,
    private _fb: FormBuilder
  ) {

    this._ddlSvc.getDdlFileUploadType().then(result => {
      this.fileTypes = result;
    })
  }

  fileTypes: Dropdownlist[] = [];
  validationForm: FormGroup;

  model: AttachFileView = new AttachFileView();

  ngOnInit() {
    this.buildForm();
  }

  buildForm() {
    this.validationForm = this._fb.group({

      // file: [null, [Validators.required]],
      attachFileTypeId: [null, [Validators.required]]

    });
  }

  fileChange(file: File[]) {
    if (file.length > 0) {
      this.model.file = file[0];
    } else {
      this.model.file = null;
    }
  }

  async save() {

    this.dialogRef.close(this.model);

  }

  close() {
    this.dialogRef.close();
  }

}