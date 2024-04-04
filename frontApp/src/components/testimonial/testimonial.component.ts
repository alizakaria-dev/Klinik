import { NgClass } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Testimonial } from '../../services/testimonial.service';

@Component({
  selector: 'app-testimonial',
  standalone: true,
  imports: [NgClass],
  templateUrl: './testimonial.component.html',
  styleUrl: './testimonial.component.css',
})
export class TestimonialComponent {
  @Input() testimonials: Testimonial[] = [];

  selectedIndex = Math.floor(this.testimonials.length / 2);

  animationDirection = '';

  selectImage(index: number): void {
    this.selectedIndex = index;
  }

  onPrevClick(): void {
    if (this.selectedIndex === 0) {
      this.selectedIndex = this.testimonials.length - 1;
      this.animationDirection = 'left';
    } else {
      this.selectedIndex--;
      this.animationDirection = 'left';
    }
  }

  onNextClick(): void {
    if (this.selectedIndex === this.testimonials.length - 1) {
      this.selectedIndex = 0;
      this.animationDirection = 'right';
    } else {
      this.selectedIndex++;
      this.animationDirection = 'right';
    }
  }
}
