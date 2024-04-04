import { HttpInterceptorFn } from '@angular/common/http';
import { UserService } from '../services/user.service';
import { inject } from '@angular/core';

export const authInterceptorInterceptor: HttpInterceptorFn = (req, next) => {
  const authToken = inject(UserService).GetTokenFromLocalStorage();

  if (authToken) {
    req = req.clone({
      headers: req.headers.append('Authorization', authToken),
    });
  }

  return next(req);
};
