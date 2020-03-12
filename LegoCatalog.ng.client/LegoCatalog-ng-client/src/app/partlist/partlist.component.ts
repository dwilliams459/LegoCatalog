import { Component, OnInit } from '@angular/core';
import { Part } from '../part';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PartSearchCriteria } from '../partSearchCriteria';
import { ApiService } from '../api.service';
import { PartResponse } from '../PartResponse';
import { FlexLayoutModule } from '@angular/flex-layout';
import { $ } from 'protractor';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DetailDialogComponent } from '../detaildialog/detaildialog.component';

@Component({
  selector: 'app-partlist',
  templateUrl: './partlist.component.html',
  styleUrls: ['./partlist.component.css']
})
export class PartlistComponent implements OnInit {
  legoParts: Part[] = [];
  part: Part;

  form: FormGroup;
  searchPartName: string;

  constructor(private fb: FormBuilder, private dialog: MatDialog, private partService: ApiService) { }

  ngOnInit() {
    this.form = this.fb.group({
    });
    this.search();
  }

  openDetailsDialog(part: Part) {
    const dialogConfig = new MatDialogConfig();

    console.log('openDetailsDialog: ' + part);
    dialogConfig.autoFocus = true;
    dialogConfig.minHeight = 400;
    dialogConfig.data = {
      imageLink: part.imageLink,
      itemName: part.itemName,
      itemId: part.itemId,
      part
    };

    const dialogRef = this.dialog.open(DetailDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
        data => console.log('Dialog output:', data)
    );
  }
  displayPartDialog(itemId: string) {
    console.log('display item id:' + itemId);
  }

  search(itemName: string = '', partName: string = '') {
    const partSearch: PartSearchCriteria = {
      partId: 0,
      itemId: itemName,
      itemName: partName,
      categoryName: '',
      page: 0,
      pageSize: 10
    };

    this.partService.getParts(partSearch).then((response: any) => {
      console.log('Response', response);
      const partlistResponse = response as PartResponse[];

      this.legoParts = [];

      partlistResponse.forEach(pr => {
        const part = this.partService.mapPartResponse(pr);
        this.legoParts.push(part);
      });
    });
  }
}
