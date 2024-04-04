import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { InputComponent } from '../../components/input/input.component';
import { Login, UserService } from '../../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, InputComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  userService = inject(UserService);
  private router = inject(Router);

  username: string;
  password: string;

  Login() {
    const login = new Login();
    login.USERNAME = this.username;
    login.PASSWORD = this.password;

    this.userService.Login(login).subscribe({
      next: (result) => {
        console.log(result);
        if (result.IsSuccess === true) {
          this.userService.SetItemInLocalStorage(result.MyResult.Token);
          this.router.navigate(['']);
        }
      },
      error: (err) => console.error(err),
    });
  }
}
