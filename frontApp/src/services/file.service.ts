import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonService, Result } from './common.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FileService {
  url: string = '';
  constructor(private http: HttpClient, private common: CommonService) {}

  UploadFile(
    files: File[],
    relKey: number,
    relField: string,
    relTable: string
  ): Observable<UploadFileResult> {
    this.url = this.common.baseUrl + 'Files/UploadFile';

    const params = new HttpParams()
      .set('relKey', relKey.toString())
      .set('relTable', relTable)
      .set('relField', relField);

    const formData = new FormData();
    for (let i = 0; i < files.length; i++) {
      formData.append(`files${i + 1}`, files[i]);
    }

    return this.http.post<UploadFileResult>(this.url, formData, {
      //headers,
      params,
    });
  }

  DeleteFile(id: number): Observable<Result> {
    this.url = this.common.baseUrl + `Files/DeleteFile?id=${id.toString()}`;
    return this.http.delete<Result>(this.url);
  }
}

export class UploadFileResult extends Result {
  override MyResult: Files;
}

export class Files {
  FILEID: number;
  REL_KEY: number;
  long: number;
  REL_TABLE: string;
  REL_FIELD: string;
  EXTENSION: string;
  URL: string;
}
