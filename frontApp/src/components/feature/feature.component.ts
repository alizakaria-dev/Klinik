import { Component, Input } from '@angular/core';
import { Feature } from '../../services/feature.service';
import { IconComponent } from '../icon/icon.component';

@Component({
  selector: 'app-feature',
  standalone: true,
  imports: [IconComponent],
  templateUrl: './feature.component.html',
  styleUrl: './feature.component.css',
})
export class FeatureComponent {
  @Input() feature: Feature;
}
