import { Component, OnInit } from "@angular/core";
import { Part } from "../part";
import { FormBuilder, FormGroup } from "@angular/forms";
import { PartSearchCriteria } from "../partSearchCriteria";
import { ApiService } from "../api.service";
//import { PartResponse } from '../PartResponse';
import { FlexLayoutModule } from "@angular/flex-layout";
import { $ } from "protractor";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { DetailDialogComponent } from "../detaildialog/detaildialog.component";
import { PageEvent } from "@angular/material/paginator";

@Component({
  selector: "app-partlist",
  templateUrl: "./partlist.component.html",
  styleUrls: ["./partlist.component.css"]
})
export class PartlistComponent implements OnInit {
  legoParts: Part[] = [];
  part: Part;

  form: FormGroup;
  searchPartName: string;
  pageIndex = 0;
  length = 100;
  pageSize = 10;
  pageSizeOptions: number[] = [5, 10, 25, 100];

  // MatPaginator Output
  pageEvent: PageEvent;

  constructor(
    private fb: FormBuilder,
    private dialog: MatDialog,
    private partService: ApiService
  ) {}

  ngOnInit() {
    this.form = this.fb.group({});
    this.search();
  }
  setPageSizeOptions(setPageSizeOptionsInput: string) {
    if (setPageSizeOptionsInput) {
      this.pageSizeOptions = setPageSizeOptionsInput
        .split(",")
        .map(str => +str);
    }
  }

  pageChanged(itemId: string, itemName: string, pageEvent?: PageEvent) {
    this.pageIndex = pageEvent.pageIndex;
    this.pageSize = pageEvent.pageSize;
    this.length = pageEvent.length;

    this.search(itemId, itemName);

    return pageEvent;
  }

  openDetailsDialog(part: Part) {
    const dialogConfig = new MatDialogConfig();

    console.log("openDetailsDialog: " + part);
    dialogConfig.autoFocus = true;
    dialogConfig.minHeight = 400;
    dialogConfig.data = {
      imageLink: part.imageLink,
      itemName: part.itemName,
      itemId: part.itemId,
      part
    };

    const dialogRef = this.dialog.open(DetailDialogComponent, dialogConfig);

    dialogRef
      .afterClosed()
      .subscribe(data => console.log("Dialog output:", data));
  }
  displayPartDialog(itemId: string) {
    console.log("display item id:" + itemId);
  }

  incrementQuantity(part: Part) {
    part.quantity++;
    this.partService.setQuantity(part, part.quantity);
  }

  decrementQuantity(part: Part) {
    part.quantity--;
    this.partService.setQuantity(part, part.quantity);
  }

  search(itemName: string = '', partName: string = '', colorOnly: boolean = false) {
    const partSearch: PartSearchCriteria = {
      partId: 0,
      itemId: itemName,
      itemName: partName,
      categoryName: '',
      colorOnly,
      page: this.pageIndex,
      pageSize: this.pageSize
    };

    this.partService.getParts(partSearch).then((response: any) => {
      console.log("Response", response);
      this.legoParts = response as Part[];

      //const partlistResponse = response as Part[];
      // partlistResponse.forEach(pr => {
      //   const part = this.partService.mapPartResponse(pr);
      //   this.legoParts.push(part);
      // });
    });
  }
}
