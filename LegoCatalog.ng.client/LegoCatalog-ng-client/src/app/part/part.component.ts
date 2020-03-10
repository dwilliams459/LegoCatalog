import { Component, OnInit } from '@angular/core';
import { Part } from '../part';
import { PartSearchCriteria } from '../PartSearchCriteria';
import { ApiService } from '../api.service';
import { PartResponse } from '../PartResponse';

@Component({
  selector: 'app-part',
  templateUrl: './part.component.html',
  styleUrls: ['./part.component.css']
})
export class PartComponent implements OnInit {
  part: Part;

  constructor(private partService: ApiService) {
  }

  ngOnInit() {
    this.getPart();
  }

  getPart() {
    let response = this.partService.getByPartId(10006).then((response: any) => {
      console.log('Response', response);
      this.part = this.partService.mapPartResponse(response);
    });
  }



}
