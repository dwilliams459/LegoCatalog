import { Component, OnInit } from '@angular/core';
import { Part } from '../part';
import { PartSearchCriteria } from '../partSearchCriteria';
import { ApiService } from '../api.service';
import { PartResponse } from '../PartResponse';
import { FormBuilder, FormGroup } from '@angular/forms';

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
              private partService: ApiService) { 
debugger;

              }

  ngOnInit() {
    this.form = this.fb.group({
    });
    debugger;
    this.search();
  }
  search() {
    const partSearch: PartSearchCriteria = {
      partId: 0,
      itemId: "",
      itemName: "Hieroglyphs",
      categoryName: "",
      page: 0,
      pageSize: 10
      //name: this.form.get('name').value,
    };

    var response = this.partService.getParts(partSearch).then((response: any) => {
      debugger;
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
