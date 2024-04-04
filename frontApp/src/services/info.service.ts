import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonService, Result } from './common.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class InfoService {
  url: string = '';
  constructor(private http: HttpClient, private common: CommonService) {}

  GetInfo(): Observable<GetInfoResult> {
    this.url = this.common.baseUrl + 'Info/GetInfo';
    return this.http.get<GetInfoResult>(this.url);
  }
}

export class Info {
  INFOID: number;
  DOCTORS: number;
  PATIENTS: number;
  STAFF: number;
  Files: File[];
}

export class GetInfoResult extends Result {
  override MyResult: Info;
}
