import { Component, OnInit } from '@angular/core';
import * as XLSX from 'xlsx';
import { ImportDataService } from '../../_service/import-data.service';
import { MessageService } from '../../_service/message.service';
import { ImportDataView, DatasView, ImportProductView, DatasProductView } from '../../_model/import-data';
import { Router } from '@angular/router';

type AOA = any[][];

@Component({
  selector: 'app-import-product',
  templateUrl: './import-product.component.html',
  styleUrls: ['./import-product.component.scss']
})
export class ImportProductComponent {

  constructor(
    private importSvc: ImportDataService,
    private _msgSvc: MessageService,
    private _router: Router
  ) { }

  data: AOA = [[, ], [, ]];
  wopts: XLSX.WritingOptions = { bookType: 'xlsx', type: 'array' };
  fileName: string = 'SheetJS.xlsx';

  
  public model: ImportProductView = new ImportProductView();
  public dataList : DatasProductView = new DatasProductView();
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
      
      this.dataList.prod_code = this.data[i][0];
      this.dataList.prod_name = this.data[i][1];
      this.dataList.uom_code  = this.data[i][2];
      this.dataList.bar_code  = this.data[i][3];
      this.dataList.entity  = this.data[i][4];
      this.dataList.pdgrp_code  = this.data[i][5];
      this.dataList.pdbrnd_code  = this.data[i][6];
      this.dataList.pdtype_code  = this.data[i][7];
      this.dataList.pddsgn_code  = this.data[i][8];
      this.dataList.pdsize_code  = this.data[i][9];
      this.dataList.pdcolor_code  = this.data[i][10];
      this.dataList.pdmisc_code  = this.data[i][11];
      this.dataList.pdmodel_code  = this.data[i][12];
      this.dataList.pdgrp_desc  = this.data[i][13];
      this.dataList.pdbrnd_desc  = this.data[i][14]; 
      this.dataList.pdtype_desc  = this.data[i][15];
      this.dataList.pddsgn_desc  = this.data[i][16];
      this.dataList.pdcolor_desc  = this.data[i][17];
      this.dataList.pdsize_desc  = this.data[i][18];
      this.dataList.pdmisc_desc  = this.data[i][19];
      this.dataList.pdmodel_desc  = this.data[i][20];
      this.dataList.unit_price  = this.data[i][21];
      

      this.importList.push(this.dataList);
      this.dataList =  new DatasProductView();
      //console.log(this.importList);
    }

    //console.log(this.importList); 

    this.model.product = this.importList;

    //console.log(this.model);
    await this.importSvc.importProduct(this.model);

    await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
    this._router.navigateByUrl('/app/import-menu');
  }

  close()
  {
    window.history.back();
  }

}
