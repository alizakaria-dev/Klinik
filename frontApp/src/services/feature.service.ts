import { Injectable } from '@angular/core';
import { CommonService, Result } from './common.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FeatureService {
  url: string = '';
  constructor(private http: HttpClient, private common: CommonService) {}

  GetFeature(): Observable<GetFeatureResult> {
    this.url = this.common.baseUrl + 'Features/GetFeature';
    return this.http.get<GetFeatureResult>(this.url);
  }
}

export class FeatureItem {
  FEATUREITEMID: number;
  FEATUREID: number;
  ICON: string;
  TEXTONE: string;
  TEXTTWO: string;
}

export class Feature {
  FEATUREID: number;
  TEXT: string;
  TITLE: string;
  FEATUREITEMS: FeatureItem[];
}

export class GetFeatureResult extends Result {
  override MyResult: Feature;
}
