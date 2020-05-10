import { AuthenticationService } from './../../_service/authentication.service';
import { AttachFileAddModalComponent } from './../attach-file-add-modal/attach-file-add-modal.component';
import { MatDialog } from '@angular/material';
import { MessageService } from './../../_service/message.service';
import { CommonService } from './../../_service/common.service';
import { AttachFileView } from './../../_model/attach-file-view';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-attach-file-update',
  templateUrl: './attach-file-update.component.html',
  styles: []
})
export class AttachFileUpdateComponent implements OnInit {

  constructor(
    private _actRoute: ActivatedRoute,
    private _commonSvc: CommonService,
    private _msgSvc: MessageService,
    private _dialog: MatDialog,
    private _authSvc: AuthenticationService
  ) { }

  refId: number = 0;
  docCode: string = "";
  datas: AttachFileView[] = [];
  user: any;

  async ngOnInit() {

    // this.refId = this._actRoute.snapshot.params.refId;
    // this.docCode = this._actRoute.snapshot.params.docCode;
    // this.user = this._authSvc.getLoginUser();

    let model: AttachFileView = new AttachFileView();
      // model.refTransactionId = this.refId;
      // model.docCode = this.docCode;
      this.datas = await this._commonSvc.postInquiryAttachFile(model);
  }

  upload() {
    const dialogRef = this._dialog.open(AttachFileAddModalComponent, {
      maxWidth: '100vw',
      maxHeight: '100vh',
      height: '100%',
      width: '100%'
    });

    dialogRef.afterClosed().subscribe(async result => {
      if (result) {
        // this.datas = result;

        // result.createUser = this.user.username;
        // result.docCode = this.docCode;
        // result.refTransactionId = this.refId;
        this.datas = await this._commonSvc.postUploadAttachFile(result);
      }
    });

  }

  view(x: AttachFileView) {
    window.open(x.fullPath);
  }

  delete(x: AttachFileView) {

    this._msgSvc.confirmPopup("ยืนยันลบไฟล์แนบ", async result => {

      if (result) {
        this.datas = await this._commonSvc.postDeleteAttachFile(x);
        this._msgSvc.successPopup("ลบข้อมูลเรียบร้อย");
      }

    })

  }

  close() {
    window.history.back();
  }

}

