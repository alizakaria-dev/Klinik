import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../services/user.service';

export const appointmentGuardGuard: CanActivateFn = (route, state) => {
  const userService = inject(UserService);
  const router = inject(Router);
  const roleId = userService.GetRoleId();
  if (!roleId) {
    router.navigate(['']);
  }
  if (roleId === 5) {
    router.navigate(['']);
  }
  return true;
};
