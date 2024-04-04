import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonService, Result } from './common.service';
import { Files } from './file.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TestimonialService {
  url: string;
  constructor(private http: HttpClient, private common: CommonService) {}

  GetTestimonials(): Observable<GetTestimonialsResult> {
    this.url = this.common.baseUrl + 'Testimonials/GetAllTestimonials';
    return this.http.get<GetTestimonialsResult>(this.url);
  }

  CreateTestimonial(
    testimonial: Testimonial
  ): Observable<CreateTestimonialResult> {
    this.url = this.common.baseUrl + 'Testimonials/CreateTestimonial';
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<CreateTestimonialResult>(this.url, testimonial, {
      headers,
    });
  }
}

export class GetTestimonialsResult extends Result {
  override MyResult: Testimonial[];
}

export class CreateTestimonialResult extends Result {
  override MyResult: Testimonial;
}

export class Testimonial {
  TESTIMONIALID: number;
  USERNAME: string;
  PROFESSION: string;
  DESCRIPTION: string;
  File: Files;
}
