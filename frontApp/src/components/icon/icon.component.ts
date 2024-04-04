import {
  Component,
  HostBinding,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-icon',
  standalone: true,
  imports: [NgClass],
  templateUrl: './icon.component.html',
  styleUrl: './icon.component.css',
})
export class IconComponent {
  @Input() iconSize: string = '';
  @Input() icon: string = '';
  @Input() iconContainerSize: string = '';
  @Input() cursorPointer: string = '';

  constructor() {}
}
