import { Component } from '@angular/core';
import { IconComponent } from '../icon/icon.component';

@Component({
  selector: 'app-card',
  standalone: true,
  imports: [IconComponent],
  templateUrl: './card.component.html',
  styleUrl: './card.component.css',
})
export class CardComponent {}
