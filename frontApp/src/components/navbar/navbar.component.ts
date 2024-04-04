import { NgClass } from '@angular/common';
import { Component, Input, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [NgClass, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  private userService = inject(UserService);
  isNavToggled = false;

  @Input() localStorageValue: string;

  @Input() roleId: number;

  toggleNav() {
    this.isNavToggled = !this.isNavToggled;
  }

  RemoveToken() {
    this.userService.logout();
    window.location.reload();
  }
}
