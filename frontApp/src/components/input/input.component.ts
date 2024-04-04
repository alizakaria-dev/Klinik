import { Component, Input, SimpleChanges, forwardRef } from '@angular/core';
import {
  ControlValueAccessor,
  FormsModule,
  NG_VALUE_ACCESSOR,
} from '@angular/forms';

@Component({
  selector: 'app-input',
  standalone: true,
  imports: [FormsModule],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => InputComponent),
      multi: true,
    },
  ],
  templateUrl: './input.component.html',
  styleUrl: './input.component.css',
})
export class InputComponent implements ControlValueAccessor {
  @Input() type: string;
  @Input() placeholder: string;
  @Input() inputName: string;
  @Input() secondType: string;
  inputValue: string;

  // ControlValueAccessor methods
  onChange: any = () => {};
  onTouched: any = () => {};

  writeValue(value: any): void {
    this.inputValue = value;
  }

  registerOnChange(fn: any): void {
    this.onChange = (value: any) => {
      fn(value);
    };
  }

  registerOnTouched(fn: any): void {
    this.onTouched = () => {
      fn();
    };
  }

  setDisabledState(isDisabled: boolean): void {
    // You can implement this method if your component supports disabling
  }
}
