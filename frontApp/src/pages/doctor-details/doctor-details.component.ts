import { Component, OnInit, inject } from '@angular/core';
import { Doctor, DoctorService } from '../../services/doctor.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { IconComponent } from '../../components/icon/icon.component';
import { InputComponent } from '../../components/input/input.component';
import { FormsModule } from '@angular/forms';
import { FileService } from '../../services/file.service';

@Component({
  selector: 'app-doctor-details',
  standalone: true,
  imports: [IconComponent, InputComponent, FormsModule],
  templateUrl: './doctor-details.component.html',
  styleUrl: './doctor-details.component.css',
})
export class DoctorDetailsComponent implements OnInit {
  doctor: Doctor;
  id: number;
  name: string;
  department: string;
  description: string;
  toggleEditMode: boolean = false;
  files: File[];

  private doctorService = inject(DoctorService);
  private route = inject(ActivatedRoute);
  private fileService = inject(FileService);

  ngOnInit(): void {
    this.route.paramMap.subscribe((params: ParamMap) => {
      this.id = +params.get('id');
    });

    this.GetDoctor(this.id);
  }

  GetDoctor(id: number) {
    this.doctorService.GetDoctor(id).subscribe({
      next: (result) => {
        console.log(result), (this.doctor = result.MyResult);
      },
      error: (err) => console.error(err),
    });
  }

  ToggleEditMode() {
    this.toggleEditMode = !this.toggleEditMode;
    this.name = this.doctor.NAME;
    this.department = this.doctor.DEPARTMENT;
    this.description = this.doctor.DESCRIPTION;
  }

  onChange(event: any) {
    const files: File[] = event.target.files;
    this.files = files;
  }

  UpdateDoctor() {
    const doc = new Doctor();
    doc.NAME = this.name;
    doc.DEPARTMENT = this.department;
    doc.DESCRIPTION = this.description;
    doc.DOCTORID = this.doctor.DOCTORID;
    doc.FACEBOOKLINK = this.doctor.FACEBOOKLINK;
    doc.TWITTERLINK = this.doctor.TWITTERLINK;
    doc.INSTAGRAMLINK = this.doctor.INSTAGRAMLINK;

    this.doctorService.UpdateDoctor(doc).subscribe({
      next: (doctorResult) => {
        console.log(doctorResult);
        if (doctorResult.IsSuccess === true && this.files) {
          this.fileService
            .UploadFile(
              this.files,
              doctorResult.MyResult.DOCTORID,
              'DoctorImage',
              'DOCTORTABLE'
            )
            .subscribe({
              next: (fileResult) => {
                console.log(fileResult);
              },
              error: (err) => console.error(err),
            });
        }
        this.GetDoctor(this.id), (this.toggleEditMode = false);
      },
      error: (err) => console.error(err),
    });
  }

  DeleteFile(fileId: number) {
    this.fileService.DeleteFile(fileId).subscribe({
      next: (result) => {
        console.log(result);
        if (result.IsSuccess === true) {
          this.GetDoctor(this.id);
        }
      },
      error: (err) => console.error(err),
    });
  }
}
