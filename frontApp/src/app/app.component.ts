import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { UserService } from '../services/user.service';
import { NavbarComponent } from '../components/navbar/navbar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, NavbarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  ngOnInit(): void {
    this.GetTokenFromLocalStorage();
    this.roleId = this.userService.GetRoleId();
  }
  private userService = inject(UserService);
  title = 'frontApp';
  localStorageValue: string;
  roleId: number;

  GetTokenFromLocalStorage() {
    this.localStorageValue = this.userService.GetTokenFromLocalStorage();
  }
}
