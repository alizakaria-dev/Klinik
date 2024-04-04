import { Component, OnInit, inject } from '@angular/core';
import {
  Appointment,
  AppointmentService,
} from '../../services/appointment.service';

@Component({
  selector: 'app-appointments',
  standalone: true,
  imports: [],
  templateUrl: './appointments.component.html',
  styleUrl: './appointments.component.css',
})
export class AppointmentsComponent implements OnInit {
  appointmentService = inject(AppointmentService);
  ngOnInit(): void {
    this.GetAppointments();
  }

  appointments: Appointment[];

  GetAppointments() {
    this.appointmentService.GetAppointments().subscribe({
      next: (result) => {
        this.appointments = result.MyResult;
        console.log(result);
      },
      error: (err) => console.error(err),
    });
  }
}
