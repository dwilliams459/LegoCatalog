import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { Part } from './part';

@Injectable({
  providedIn: 'root'
})
export class ServerService {

    constructor(private http: HttpClient) { //}, public oktaAuth: OktaAuthService) {
    }

    private async request(method: string, url: string, data?: any) {

      const result = this.http.request(method, url, {
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

    getEventList() {
      return this.request('GET', `${environment.serverUrl}/event`);
    }

    getEvent(event) {
      return this.request('GET', `${environment.serverUrl}/event/${event.id}`);
    }

    createEvent(event) {
      return this.request('POST', `${environment.serverUrl}/event`, event);
    }

    updateEvent(event) {
      return this.request('PUT', `${environment.serverUrl}/event`, event);
    }

    deleteEvent(event) {
      return this.request('DELETE', `${environment.serverUrl}/event/${event.id}`);
    }

    getPartById(partId: number) {
      return this.request('GET', `${environment.serverUrl}/part/${partId}`);
    }

}