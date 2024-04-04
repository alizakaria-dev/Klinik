import { Component, Input } from '@angular/core';
import { Service } from '../../services/service.service';
import { IconComponent } from '../icon/icon.component';

@Component({
  selector: 'app-healthcare',
  standalone: true,
  imports: [IconComponent],
  templateUrl: './healthcare.component.html',
  styleUrl: './healthcare.component.css',
})
export class HealthcareComponent {
  @Input() services: Service[];
}
