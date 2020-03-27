import { AttachFileView } from './../../_model/attach-file-view';
import { CommonService } from './../../_service/common.service';
import { Component, OnInit, Input, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-attach-file-view',
  templateUrl: './attach-file-view.component.html',
  styles: []
})
export class AttachFileViewComponent implements OnInit {

  constructor(
    private _commonSvc: CommonService
  ) { }

  @Input() refId: number = 0;
  @Input() docCode: string = "";
  @Input() statusId: string = "";
  @Input() UPLOAD: boolean = false;

  datas: AttachFileView[] = [];

  ngOnInit() {
  }

  async ngOnChanges(changes: SimpleChanges) {

    if (changes['refId']) {

      let model: AttachFileView = new AttachFileView();
      model.refTransactionId = this.refId;
      model.docCode = this.docCode;
      this.datas = await this._commonSvc.postInquiryAttachFile(model);

    }

  }

  view(x: AttachFileView) {
    window.open(x.fullPath);
  }

}

