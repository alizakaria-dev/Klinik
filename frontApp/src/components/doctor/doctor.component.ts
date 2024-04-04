import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Doctor } from '../../services/doctor.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-doctor',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './doctor.component.html',
  styleUrl: './doctor.component.css',
})
export class DoctorComponent {
  @Input() doctors: Doctor[];
  @Input() roleId: number;
  @Output() doctorId: EventEmitter<number> = new EventEmitter();

  SendId(id: number) {
    this.doctorId.emit(id);
  }
}
