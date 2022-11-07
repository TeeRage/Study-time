import { Component, OnInit } from '@angular/core';
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

  //Use AccountService as injected service for login
  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
  }

  //two-way communication action, for user login on button onclick using form property (ngSubmit)="login()"
  login() {
    this.accountService.login(this.model).subscribe(response => {
      console.log("Login success");
      console.log(response);
    }, error => {
      console.log("Login error");
      console.log(error);
    });
  }

  logout() {
    this.accountService.logout(); //use logout from AccountService
  }
}
