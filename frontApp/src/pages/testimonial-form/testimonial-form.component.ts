import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import {
  Testimonial,
  TestimonialService,
} from '../../services/testimonial.service';
import { FileService } from '../../services/file.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-testimonial-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './testimonial-form.component.html',
  styleUrl: './testimonial-form.component.css',
})
export class TestimonialFormComponent {
  testimonialsForm: FormGroup;

  private testimonialService = inject(TestimonialService);
  private fileService = inject(FileService);
  private router = inject(Router);

  file: File[];

  constructor(private fb: FormBuilder) {
    this.testimonialsForm = this.fb.group({
      USERNAME: ['', [Validators.required, Validators.minLength(3)]],
      PROFESSION: ['', Validators.required],
      DESCRIPTION: ['', [Validators.required, Validators.maxLength(200)]],
    });
  }

  onChange(event: any) {
    const file: File[] = event.target.files;
    this.file = file;
  }

  onSubmit(form: FormGroup) {
    const testimonial = new Testimonial();
    testimonial.DESCRIPTION = form.value.DESCRIPTION;
    testimonial.USERNAME = form.value.USERNAME;
    testimonial.PROFESSION = form.value.PROFESSION;

    this.testimonialService.CreateTestimonial(testimonial).subscribe({
      next: (result) => {
        console.log(result);
        if (result.IsSuccess === true && this.file) {
          const relTable = 'TESTIMONIALTABLE';
          const relField = 'USERTESTIMONIALIMAGE';
          this.fileService
            .UploadFile(
              this.file,
              result.MyResult.TESTIMONIALID,
              relField,
              relTable
            )
            .subscribe({
              next: (result) => console.log(result),
              error: (err) => console.error(err),
            });
        }
        this.router.navigate(['']);
      },
      error: (err) => console.error(err),
    });
  }
}
