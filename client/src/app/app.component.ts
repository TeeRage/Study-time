import { Component, OnInit } from '@angular/core';
import { User } from './_models/user'; //User model
import { AccountService } from './_services/account.service';

//Decorator that gives normal classess some more extra powers (our class AppComponent is now an angular component)
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit{
  title = 'My awesome app';
  users: any; //Users can be any type, type safety is "turned off"

  //inject http client with constructor
  constructor(private accountService: AccountService) {}

  //OnInitialize, get list of users and set current logged in user
  ngOnInit() {
    this.setCurrentUser();
  }

  //Get and set the user item saved to local storage when user logged in
  setCurrentUser() {
    const user : User = JSON.parse(localStorage.getItem('user')); //Uses user keyword set in account.service.ts
    this.accountService.setCurrentUser(user);
  }
}