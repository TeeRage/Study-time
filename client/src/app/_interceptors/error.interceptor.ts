//Interceptor for errors, needed to add into app.module providers
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  //Inject router for redirecting the user to error page, toaster for error messages
  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    
    return next.handle(request).pipe(
      catchError(error => {
        if(error) { //Check if we have error, and do things accordingly to error status
          switch (error.status) {
            case 400:
              if(error.error.errors) {
                const modalStateErrors = [];
                for(const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modalStateErrors.push(error.error.errors[key])
                  }
                } //to get flat() working, needs es2019 added to tsconfig.json libraries
                throw modalStateErrors.flat(); //Pass the errors as flat (username and password validations)
              }
              else{ //A bug in Angular/something, the statusText is 'OK' even it shouldn't be -> need if for showing proper error messages
                //this.toastr.error(error.statusText, error.status);
                this.toastr.error(error.statusText === "OK" ? "Bad Request" : error.statusText, error.status);
              }
              break;
              case 401: //A bug in Angular/something, the statusText is 'OK' even it shouldn't be -> need if for showing proper error messages
                //this.toastr.error(error.statusText, error.status);
                this.toastr.error(error.statusText === "OK" ? "Unauthorised" : error.statusText, error.status);
                break;
              case 404:
                this.router.navigateByUrl('/not-found');
                break;
              case 500:
                const navigationExtras: NavigationExtras = {state: {error: error.error}};
                this.router.navigateByUrl('/server-error', navigationExtras);
                break;
            default:
                this.toastr.error('Something unexpected went wrong');
                console.log(error);
              break;
          }
        }
        return throwError(error);
      })
    )
  }
}
