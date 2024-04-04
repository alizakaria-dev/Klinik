import { Component, inject } from '@angular/core';
import { FileService } from '../../services/file.service';
import { Doctor, DoctorService } from '../../services/doctor.service';
import { FormsModule } from '@angular/forms';
import { InputComponent } from '../../components/input/input.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-doctor-form',
  standalone: true,
  imports: [FormsModule, InputComponent],
  templateUrl: './doctor-form.component.html',
  styleUrl: './doctor-form.component.css',
})
export class DoctorFormComponent {
  private fileService = inject(FileService);
  private doctorService = inject(DoctorService);
  private router = inject(Router);

  name: string;
  department: string;
  facebookLink: string;
  twitterLink: string;
  instagramLink: string;
  description: string;
  file: File[] | null = null;

  onChange(event: any) {
    const files: File[] = event.target.files;
    this.file = files;
  }

  Upload() {
    const doctor = new Doctor();
    doctor.NAME = this.name;
    doctor.DEPARTMENT = this.department;
    doctor.FACEBOOKLINK = this.facebookLink;
    doctor.TWITTERLINK = this.twitterLink;
    doctor.INSTAGRAMLINK = this.instagramLink;
    doctor.DESCRIPTION = this.description;

    this.doctorService.AddDoctor(doctor).subscribe({
      next: (doctorResult) => {
        console.log(doctorResult);
        if (doctorResult.IsSuccess === true) {
          this.fileService
            .UploadFile(
              this.file,
              doctorResult.MyResult.DOCTORID,
              'DoctorImage',
              'DOCTORTABLE'
            )
            .subscribe({
              next: (fileResult) => {
                console.log(fileResult);
                if (fileResult.IsSuccess === true) {
                  this.router.navigate(['']);
                }
              },
              error: (err) => console.error(err),
            });
        }
      },
    });
  }
}
