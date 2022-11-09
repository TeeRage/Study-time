//Guard to check if we can activate a route or not
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  //Constructor so that we can inject account service and toastr
  constructor(private accountService: AccountService, private toastr: ToastrService){};

  //Return observable true, if we have a user logging in
  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map(user => {
        if(user) return true;
        this.toastr.error('You shall not pass!'); //Unauthorized user, a Balrog on a bridge of Khazad dum
      })
    );
  }  
}
