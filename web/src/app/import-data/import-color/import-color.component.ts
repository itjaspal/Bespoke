import { Component, OnInit } from '@angular/core';
import * as XLSX from 'xlsx';
import { ImportDataService } from '../../_service/import-data.service';
import { MessageService } from '../../_service/message.service';
import { ImportDataView, DatasView } from '../../_model/import-data';
import { Router } from '@angular/router';

type AOA = any[][];

@Component({
  selector: 'app-import-color',
  templateUrl: './import-color.component.html',
  styleUrls: ['./import-color.component.scss']
})
export class ImportColorComponent  {

  constructor(
    private importSvc: ImportDataService,
    private _msgSvc: MessageService,
    private _router: Router
  ) { }

  data: AOA = [[, ], [, ]];
  wopts: XLSX.WritingOptions = { bookType: 'xlsx', type: 'array' };
  fileName: string = 'SheetJS.xlsx';

  
  public model: ImportDataView = new ImportDataView();
  public dataList : DatasView = new DatasView();
  public code : any;
  public name : any;
  public importList : any;

  onFileChange(evt: any) {
    /* wire up file reader */
    const target: DataTransfer = <DataTransfer>(evt.target);
    if (target.files.length !== 1) throw new Error('Cannot use multiple files');
    const reader: FileReader = new FileReader();
    reader.onload = (e: any) => {
      /* read workbook */
      const bstr: string = e.target.result;
      const wb: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' });

      /* grab first sheet */
      const wsname: string = wb.SheetNames[0];
      const ws: XLSX.WorkSheet = wb.Sheets[wsname];

      /* save data */
      this.data = <AOA>(XLSX.utils.sheet_to_json(ws, { header: 1 }));
      console.log(this.data);
    };
    reader.readAsBinaryString(target.files[0]);
  }


  async import(): Promise<void> {

    this.importList = [];
    console.log(this.importList);

    for(var i=0;i<this.data.length;i++)
    {
      
      this.dataList.code = this.data[i][0];
      this.dataList.name= this.data[i][1];

      this.importList.push(this.dataList);
      this.dataList =  new DatasView();
      //console.log(this.importList);
    }

    //console.log(this.importList); 

    this.model.datas = this.importList;

    //console.log(this.model);
    await this.importSvc.importColor(this.model);

    await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
    this._router.navigateByUrl('/app/import-menu');
  }

  close()
  {
    window.history.back();
  }
}
