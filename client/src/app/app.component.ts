import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

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
  constructor(private http: HttpClient) {}

  //OnInitialize, get users
  ngOnInit() {
    this.getUsers();
  }

  //Get users from local host api containing SQLite dummy data
  getUsers(){
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: response => this.users = response,
      error: error => console.log(error)
    })
  }
}