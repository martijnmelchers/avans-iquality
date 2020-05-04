import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { flatMap } from 'rxjs/operators';
import { AuthenticationService } from "@IQuality/core/services/authentication.service";

@Injectable()
export class JwtHttpInterceptor implements HttpInterceptor {
  constructor(private readonly authService: AuthenticationService) {
  }

  public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let clone: HttpRequest<any>;
    if (!this.authService) return next.handle(request);

    if (this.authService.encodedToken && !request.headers.get('disableAuthentication')) {
      return of(null).pipe(
        flatMap(() => {
          const headers = request.headers.append('Authorization', `Bearer ${this.authService.encodedToken}`);
          clone = request.clone({ headers: headers });
          return next.handle(clone);
        }));
    } else {
      const headers = request.headers.delete('disableAuthentication');
      clone = request.clone({ headers: headers });
      return next.handle(clone);
    }
  }
}
