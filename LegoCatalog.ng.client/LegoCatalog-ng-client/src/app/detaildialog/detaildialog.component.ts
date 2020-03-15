import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Part } from '../part';

@Component({
  selector: 'app-detaildialog',
  templateUrl: './detaildialog.component.html',
  styleUrls: ['./detaildialog.component.css']
})
export class DetailDialogComponent implements OnInit {
  form: FormGroup;
  description: string;
  part: Part;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<DetailDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data
  ) {
    this.part = data.part;
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      itemName: [this.part.itemName, []],
      imageLink: this.part.imageLink,
      itemId: this.part.itemId
    });
  }

  save() {
    this.dialogRef.close(this.form.value);
  }

  close() {
    this.dialogRef.close();
  }
}
