import {
  ChangeDetectorRef,
  Component,
  Input,
  OnInit,
  inject,
  signal,
} from '@angular/core';
import { NavbarComponent } from '../../../src/components/navbar/navbar.component';
import { InfoComponent } from '../../../src/components/info/info.component';
import { AboutComponent } from '../../../src/components/about/about.component';
import { HealthcareComponent } from '../../../src/components/healthcare/healthcare.component';
import { FeatureComponent } from '../../../src/components/feature/feature.component';
import { DoctorComponent } from '../../../src/components/doctor/doctor.component';
import { IconComponent } from '../../../src/components/icon/icon.component';
import { FooterComponent } from '../../../src/components/footer/footer.component';
import { Service, ServiceService } from '../../services/service.service';
import { Feature, FeatureService } from '../../services/feature.service';
import { Doctor, DoctorService } from '../../services/doctor.service';
import { AppointmentComponent } from '../../../src/components/appointment/appointment.component';
import { CarouselComponent } from '../../../src/components/carousel/carousel.component';
import { Info, InfoService } from '../../services/info.service';
import { TestimonialComponent } from '../../components/testimonial/testimonial.component';
import {
  Appointment,
  AppointmentService,
} from '../../services/appointment.service';
import { UserService } from '../../services/user.service';
import {
  Testimonial,
  TestimonialService,
} from '../../services/testimonial.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    NavbarComponent,
    InfoComponent,
    AboutComponent,
    HealthcareComponent,
    FeatureComponent,
    DoctorComponent,
    IconComponent,
    AppointmentComponent,
    FooterComponent,
    CarouselComponent,
    TestimonialComponent,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  private serviceService = inject(ServiceService);
  private featureService = inject(FeatureService);
  private doctorService = inject(DoctorService);
  private infoService = inject(InfoService);
  private appointmentService = inject(AppointmentService);
  private userService = inject(UserService);
  private testimonialService = inject(TestimonialService);

  services = signal<Service[]>([]);
  features = signal<Feature>({
    FEATUREID: 0,
    TEXT: '',
    TITLE: '',
    FEATUREITEMS: [],
  });
  doctors = signal<Doctor[]>([]);

  info = signal<Info>({
    INFOID: 0,
    DOCTORS: 0,
    PATIENTS: 0,
    STAFF: 0,
    Files: [],
  });

  testimonials = signal<Testimonial[]>([]);

  selectedDoctorId: number;

  ngOnInit(): void {
    this.GetAllServices();
    this.GetFeature();
    this.GetDoctors();
    this.GetInfo();
    this.GetTokenFromLocalStorage();
    this.GetTestimonials();
    this.roleId = this.userService.GetRoleId();
  }

  receivedAppointment: Appointment;

  isFormLoading: boolean;

  localStorageValue: string;

  decodedToken: any;

  roleId: number;

  RecievedAppointmentObject(appointment: Appointment) {
    this.receivedAppointment = appointment;
    this.CreateAppointment();
  }

  GetAllServices() {
    this.serviceService.GetAllServices().subscribe({
      next: (result) => {
        this.services.set(result.MyResult);
      },
      error: (err) => console.error(err),
    });
  }

  GetFeature() {
    this.featureService.GetFeature().subscribe({
      next: (result) => {
        this.features.set(result.MyResult);
      },
      error: (err) => console.error(err),
    });
  }

  GetDoctors() {
    this.doctorService.GetDoctors().subscribe({
      next: (result) => {
        this.doctors.set(result.MyResult);
      },
      error: (err) => console.error(err),
    });
  }

  GetInfo() {
    this.infoService.GetInfo().subscribe({
      next: (result) => {
        this.info.set(result.MyResult);
      },
      error: (err) => console.error(err),
    });
  }

  CreateAppointment() {
    this.isFormLoading = true;
    this.appointmentService
      .CreateAppointment(this.receivedAppointment)
      .subscribe({
        next: (result) => {
          console.log(result);
        },
        error: (err) => console.error(err),
        complete: () => (this.isFormLoading = false),
      });
  }

  onDoctorSelected(id: number) {
    this.selectedDoctorId = id;
    this.DeleteDoctor(this.selectedDoctorId);
  }

  DeleteDoctor(id: number) {
    this.doctorService.DeleteDoctor(id).subscribe({
      next: (result) => {
        if (result.IsSuccess === true) {
          this.GetDoctors();
        }
      },
      error: (err) => console.error(err),
    });
  }

  GetTokenFromLocalStorage() {
    this.localStorageValue = this.userService.GetTokenFromLocalStorage();
  }

  GetTestimonials() {
    this.testimonialService.GetTestimonials().subscribe({
      next: (result) => {
        this.testimonials.set(result.MyResult);
      },
      error: (err) => console.error(err),
    });
  }
}
