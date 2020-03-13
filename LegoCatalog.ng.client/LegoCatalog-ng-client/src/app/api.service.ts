import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
//import { PartResponse } from './PartResponse';
import { Part } from './part';
import { PartSearchCriteria } from './partSearchCriteria';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private httpClient: HttpClient) { }

  private async jsonRequest(method: string, url: string, data?: any) {

    const result = this.httpClient.request(method, url, {
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

  public get() {
    return this.httpClient.get(environment.serverUrl);
  }

  getByPartId(partId: number = 0) {
    let id = '';
    if (partId && partId > 0) {
      id = partId.toString();
    }
    return this.jsonRequest('GET', `${environment.serverUrl}/part/partId/${id}`);
  }

  getParts(partSearch: PartSearchCriteria) {
    return this.httpClient.post(`${environment.serverUrl}/part/search`, partSearch).toPromise();
  }

  // mapPartResponse(response: PartResponse): Part {
  //   const partResponse = response as PartResponse;
  //   const part = new Part();

  //   part.partId = partResponse.partId;
  //   part.itemId = partResponse.itemId;
  //   part.itemName = partResponse.itemName;
  //   part.categoryId = partResponse.categoryId;
  //   part.categoryName = partResponse.categoryName;
  //   part.itemWeight = partResponse.itemWeight;
  //   part.itemDimensionX = partResponse.itemDimensionX;
  //   part.itemDimensionY = partResponse.itemDimensionY;
  //   part.itemDimensionZ = partResponse.itemDimensionX;
  //   part.imageLink = partResponse.imageLink;
  //   part.iconLink = partResponse.iconLink;
  //   part.quantity = partResponse.quantity;
  //   part.colorCount = partResponse.colorCount;

  //   return part;
  // }
}
