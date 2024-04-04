import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonService, Result } from './common.service';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ServiceService {
  constructor(private http: HttpClient, private common: CommonService) {}
  url: string = '';
  GetAllServices(): Observable<GetAllServicesResult> {
    this.url = this.common.baseUrl + 'Services/GetAllServices';
    return this.http.get<GetAllServicesResult>(this.url);
  }
}

export class Service {
  SERVICEID: number;
  ICON: string;
  TITLE: string;
  TEXT: string;
}

export class GetAllServicesResult extends Result {
  override MyResult: Service[];
}
