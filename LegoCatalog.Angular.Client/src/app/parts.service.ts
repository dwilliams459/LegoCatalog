import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Part } from './part';
import { partSearchCriteria } from './PartSearchCriteria';
import { PartResponse } from './PartResponse';

@Injectable({
  providedIn: 'root'
})
export class PartsService {

  constructor(private client: HttpClient) { //}, public oktaAuth: OktaAuthService) {
  }

  private async jsonRequest(method: string, url: string, data?: any) {

    const result = this.client.request(method, url, {
      body: data,
      responseType: 'json',
      observe: 'body',
      headers: {
      }
    });

    return new Promise((resolve, reject) => {
      result.subscribe(resolve, reject);
    });
  }

  getByPartId(partId: number = 0) {
    var id = ""; 
    if (partId && partId > 0)
    {
      id = partId.toString();
    }
    return this.jsonRequest("GET", `${environment.serverUrl}/part/partId/${id}`)
  }

  getParts(partSearch: partSearchCriteria) {
    return this.client.post(`${environment.serverUrl}/part/search`, partSearch).toPromise();
  }

  mapPartResponse(response: PartResponse): Part {
    var partResponse = <PartResponse>response;
    var part = new Part();

    part.partId = partResponse.partId;
    part.itemId = partResponse.itemId;
    part.itemName = partResponse.itemName;
    part.category = partResponse.category.categoryId;
    part.categoryName = partResponse.category.name;
    part.weight = partResponse.itemWeight;
    part.dimensionX = partResponse.dimensionX;
    part.dimensionY = partResponse.dimensionY;
    part.dimensionZ = partResponse.dimensionX;
    part.imageLink = partResponse.imageLink;
    part.iconLink = partResponse.iconLink;

    return part;
  }
}
