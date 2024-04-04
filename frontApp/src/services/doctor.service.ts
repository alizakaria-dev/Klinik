import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonService, Result } from './common.service';
import { Files } from './file.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DoctorService {
  url: string = '';
  constructor(private http: HttpClient, private common: CommonService) {}

  GetDoctors(): Observable<GetDoctorsResult> {
    this.url = this.common.baseUrl + 'Doctors/GetAllDoctors';
    return this.http.get<GetDoctorsResult>(this.url);
  }

  GetDoctor(id: number): Observable<GetDoctorResult> {
    this.url = this.common.baseUrl + `Doctors/GetDoctorById?id=${id}`;
    return this.http.get<GetDoctorResult>(this.url);
  }

  AddDoctor(doctor: Doctor): Observable<AddDoctorResult> {
    this.url = this.common.baseUrl + 'Doctors/CreateDoctor';
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<AddDoctorResult>(this.url, doctor, { headers });
  }

  UpdateDoctor(doctor: Doctor): Observable<UpdateDoctorResult> {
    this.url = this.common.baseUrl + 'Doctors/UpdateDoctor';
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<UpdateDoctorResult>(this.url, doctor, { headers });
  }

  DeleteDoctor(id: number): Observable<Result> {
    this.url = this.common.baseUrl + 'Doctors/DeleteDoctor';
    let params = new HttpParams();
    params = params.set('id', id.toString());
    return this.http.delete<Result>(this.url, { params });
  }
}

export class Doctor {
  DOCTORID: number;
  NAME: string;
  DEPARTMENT: string;
  FACEBOOKLINK: string;
  TWITTERLINK: string;
  INSTAGRAMLINK: string;
  DESCRIPTION: string;
  Files: Files[];
}

export class GetDoctorsResult extends Result {
  override MyResult: Doctor[];
}
export class AddDoctorResult extends Result {
  override MyResult: Doctor;
}
export class UpdateDoctorResult extends Result {
  override MyResult: Doctor;
}
export class GetDoctorResult extends Result {
  override MyResult: Doctor;
}
