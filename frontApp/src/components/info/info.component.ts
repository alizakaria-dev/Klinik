import { Component, Input } from '@angular/core';
import { Info } from '../../services/info.service';
import { CarouselComponent } from '../carousel/carousel.component';

@Component({
  selector: 'app-info',
  standalone: true,
  imports: [CarouselComponent],
  templateUrl: './info.component.html',
  styleUrl: './info.component.css',
})
export class InfoComponent {
  @Input() info: Info;
}
