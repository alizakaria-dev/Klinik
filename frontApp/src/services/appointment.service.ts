import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonService, Result } from './common.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AppointmentService {
  url: string = '';
  constructor(private http: HttpClient, private common: CommonService) {}

  CreateAppointment(
    appointment: Appointment
  ): Observable<CreateAppointmentResult> {
    this.url = this.common.baseUrl + 'Appointments/CreateAppointment';
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<CreateAppointmentResult>(this.url, appointment, {
      headers,
    });
  }

  GetAppointments(): Observable<GetAppointmentsResult> {
    this.url = this.common.baseUrl + 'Appointments/GetAllAppointments';
    return this.http.get<GetAppointmentsResult>(this.url);
  }
}

export class Appointment {
  APPOINTMENTID: number;
  NAME: string;
  EMAIL: string;
  DOCTOR: string;
  MOBILE: string;
  DESCRIPTION: string;
  DATE: Date;
  TIME: string;
}

export class GetAppointmentsResult extends Result {
  override MyResult: Appointment[];
}

export class CreateAppointmentResult extends Result {
  override MyResult: Appointment;
}
