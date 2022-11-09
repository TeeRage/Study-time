import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

//This tells what component name to use for this component in app.component.html
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  
  model: any = {} //two-way communication model, for user login 

  //Use AccountService and Angular Router as injected service for login
  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  //Two-way communication action, for user login on button onclick using form property (ngSubmit)="login()"
  login() {
    this.accountService.login(this.model).subscribe(response => {
      this.router.navigateByUrl('/members'); //Navigate to members list after login
    }, error => {
      console.log(error);
      this.toastr.error(error.error)
    });
  }

  logout() {
    this.accountService.logout(); //Use logout from AccountService
    this.router.navigateByUrl('/'); //Navigate to home page after logout
  }
}
