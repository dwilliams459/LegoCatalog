import { Component, OnInit } from '@angular/core';
import { Part } from '../part';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PartSearchCriteria } from '../partSearchCriteria';
import { ApiService } from '../api.service';
import { FlexLayoutModule } from '@angular/flex-layout';
import { $ } from 'protractor';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatCheckbox } from '@angular/material/checkbox';
import { DetailDialogComponent } from '../detaildialog/detaildialog.component';
import { PageEvent } from '@angular/material/paginator';
import { ThemePalette } from '@angular/material/core';
import { ProgressSpinnerMode } from '@angular/material/progress-spinner';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-partlist',
  templateUrl: './partlist.component.html',
  styleUrls: ['./partlist.component.css']
})
export class PartlistComponent implements OnInit {
  legoParts: Part[] = [];

  categories: string[] = [];

  part: Part;

  form: FormGroup;
  pageIndex = 0;
  length = 1000;
  pageSize = 10;
  pageSizeOptions: number[] = [6, 10, 36, 100];

  searchPartName: string;
  displayColors = false;
  searchItemId: string;
  showOnlyColors = false;
  category: string;

  sizeX?: number;
  sizeY?: number;
  sizeZ?: number;

  color: ThemePalette = 'primary';
  mode: ProgressSpinnerMode = 'indeterminate';
  value = 50;
  loading: boolean = false;

  // MatPaginator Output
  pageEvent: PageEvent;

  constructor(
    private fb: FormBuilder,
    private dialog: MatDialog,
    private partService: ApiService,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit() {
    this.form = this.fb.group({});
    this.search();
    this.partService.getCategories().then((response: any) => {
      this.categories = response as string[];
    });
  }

  setPageSizeOptions(setPageSizeOptionsInput: string) {
    if (setPageSizeOptionsInput) {
      this.pageSizeOptions = setPageSizeOptionsInput
        .split(',')
        .map(str => +str);
    }
  }

  sanitizeImageUrl(imageUrl: string): SafeUrl {
    return this.sanitizer.bypassSecurityTrustUrl(imageUrl);
  }

  pageChanged(pageEvent?: PageEvent) {
    this.pageIndex = pageEvent.pageIndex;
    this.pageSize = pageEvent.pageSize;
    this.length = pageEvent.length;

    this.search();

    return pageEvent;
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

    dialogRef
      .afterClosed()
      .subscribe(data => console.log('Dialog output:', data));
  }
  displayPartDialog(itemId: string) {
    console.log('display item id:' + itemId);
  }

  incrementQuantity(part: Part) {
    part.quantity++;
    this.partService.setQuantity(part, part.quantity);
  }

  setQuantity(part: Part, quantity?: number) {
    part.quantity = quantity;
    this.partService.setQuantity(part, part.quantity);
  }

  decrementQuantity(part: Part) {
    part.quantity--;
    this.partService.setQuantity(part, part.quantity);
  }

  clearSearch() {
    this.searchItemId = '';
    this.searchPartName = '';
    this.category = '';
    this.showOnlyColors = false;
    this.displayColors = false;
    this.sizeX = null;
    this.sizeY = null;
    this.sizeZ = null;
  }

  search(itemName: string = '', partName: string = '', colorOnly: boolean = false, category: string = '') {
    console.log('colorOnly: ' + colorOnly);

    this.loading = true;
    const partSearch: PartSearchCriteria = {
      partId: 0,
      itemId: this.searchItemId,
      itemName: this.searchPartName,
      categoryName: this.category,
      colorOnly: this.showOnlyColors,
      displayColors: this.displayColors,
      page: this.pageIndex,
      pageSize: this.pageSize,
      sizeX: this.sizeX,
      sizeY: this.sizeY,
      sizeZ: this.sizeZ
    };

    this.partService.getParts(partSearch).then((response: any) => {
      console.log('Response', response);
      this.legoParts = response as Part[];
      this.loading = false;
    });
  }
}
