import { Routes } from '@angular/router';
import { HomeComponent } from '../pages/home/home.component';
import { AppointmentsComponent } from '../pages/appointments/appointments.component';
import { DoctorFormComponent } from '../pages/doctor-form/doctor-form.component';
import { DoctorDetailsComponent } from '../pages/doctor-details/doctor-details.component';
import { LoginComponent } from '../pages/login/login.component';
import { appointmentGuardGuard } from '../guards/appointment-guard.guard';
import { TestimonialFormComponent } from '../pages/testimonial-form/testimonial-form.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: 'appointments',
    loadComponent: () =>
      import('../pages/appointments/appointments.component').then(
        (mod) => mod.AppointmentsComponent
      ),
    canActivate: [appointmentGuardGuard],
  },
  {
    path: 'doctorform',
    loadComponent: () =>
      import('../pages/doctor-form/doctor-form.component').then(
        (mod) => mod.DoctorFormComponent
      ),
  },
  { path: 'doctordetails/:id', component: DoctorDetailsComponent },
  { path: 'login', component: LoginComponent },
  {
    path: 'testimonialform',
    loadComponent: () =>
      import('../pages/testimonial-form/testimonial-form.component').then(
        (mod) => mod.TestimonialFormComponent
      ),
  },
];
