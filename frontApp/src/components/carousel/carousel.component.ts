import { Component, Input } from '@angular/core';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { NgClass } from '@angular/common';

interface carouselImage {
  imageSrc: string;
  imageAlt: string;
}

@Component({
  selector: 'app-carousel',
  standalone: true,
  imports: [CarouselModule, NgClass],
  templateUrl: './carousel.component.html',
  styleUrl: './carousel.component.css',
})
export class CarouselComponent {
  @Input() images: carouselImage[] = [];
  @Input() indicators = true;
  @Input() controls = true;
  selectedIndex = 0;
  // @Input() files: File[];
  // customOptions: OwlOptions = {
  //   loop: true,
  //   mouseDrag: true,
  //   touchDrag: true,
  //   pullDrag: true,
  //   dots: true,
  //   navSpeed: 700,
  //   navText: ['next', 'prev'],
  //   responsive: {
  //     0: {
  //       items: 1,
  //     },
  //   },
  //   nav: true,
  // };

  selectImage(index: number): void {
    this.selectedIndex = index;
  }

  onPrevClick(): void {
    if (this.selectedIndex === 0) {
      this.selectedIndex = this.images.length - 1;
    } else {
      this.selectedIndex--;
    }
  }

  onNextClick(): void {
    if (this.selectedIndex === this.images.length - 1) {
      this.selectedIndex = 0;
    } else {
      this.selectedIndex++;
    }
  }
}
