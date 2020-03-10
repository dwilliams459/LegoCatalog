import { Component, OnInit } from '@angular/core';
import { Part } from '../part';
import { FormGroup, FormBuilder } from '@angular/forms';
import { partSearchCriteria } from '../PartSearchCriteria';
import { PartsService } from '../parts.service';
import { PartResponse } from '../PartResponse';

@Component({
  selector: 'app-partlist',
  templateUrl: './partlist.component.html',
  styleUrls: ['./partlist.component.css']
})
export class PartlistComponent implements OnInit {
  legoParts: Part[] = [];
  part: Part;

  form: FormGroup;

  constructor(private fb: FormBuilder,
              private partService: PartsService) { }

  ngOnInit() {
    this.form = this.fb.group({
    });
    this.search();
  }

  search() {
    const partSearch: partSearchCriteria = {
      partId: 0,
      itemId: "",
      itemName: "Hieroglyphs",
      categoryName: "",
      page: 0,
      pageSize: 10
      //name: this.form.get('name').value,
    };

    var response = this.partService.getParts(partSearch).then((response: any) => {
      console.log('Response', response);
      const partlistResponse = <PartResponse[]>response;

      this.legoParts = [];

      partlistResponse.forEach(pr => {
        let part = this.partService.mapPartResponse(pr);
        this.legoParts.push(part);
      });
    });
  }
}
