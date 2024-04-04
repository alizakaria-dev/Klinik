import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Appointment } from '../../services/appointment.service';
import { InputComponent } from '../input/input.component';

@Component({
  selector: 'app-appointment',
  standalone: true,
  imports: [FormsModule, InputComponent],
  templateUrl: './appointment.component.html',
  styleUrl: './appointment.component.css',
})
export class AppointmentComponent {
  @Output() appointmentObject = new EventEmitter<Appointment>();
  @Input() isFormLoading: boolean;

  name: string;
  email: string;
  mobile: string;
  doctor: string;
  description: string;
  date: Date;
  time: string;
  inputType: string;

  SendAppointmentObject() {
    const appointment: Appointment = {
      APPOINTMENTID: 0,
      NAME: this.name,
      EMAIL: this.email,
      DOCTOR: this.doctor,
      MOBILE: this.mobile,
      DESCRIPTION: this.description,
      DATE: this.date,
      TIME: this.time,
    };
    this.appointmentObject.emit(appointment);
  }
}
