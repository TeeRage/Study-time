import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { User } from '../_models/user'; //User model for response
import { ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';

//This service can be injected into other components or services in this application
//In old versions, needed to be added to the app.module.ts file as providers, but no more!
@Injectable({
  //Singleton: provides this service as long as application is "up" (user closes or moves away from app)
  providedIn: 'root' 
})

export class AccountService {
  
  //baseUrl: 'https://localhost:5001/api/'; //this does not work with http post (anymore), needs string keyword
  //baseURL: string = 'https://localhost:5001/api/';
  baseUrl: string = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1); //Observable to store one (1) user info as type User
  currentUser$ = this.currentUserSource.asObservable(); //Because observable, add $ sign

  constructor(private http: HttpClient) { }

  //Login, gets user information as JSON for the POST body
  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model).pipe( //rxJS pipe for observable
      map((response: User) => { //Response as User interface model specified in _models folder
        const user = response; //For showing that this is all about response.
        if(user) {//Check if get any users and if we do, set keyword and stringify them, populates in local storage
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user); //read next user item as observables item
        }
      })
    )
  }

  //Register new user using register form in home page/register component
  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if(user) {//Check if get any users and if we do, set keyword and stringify them, populates in local storage
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user); //read next user item as observables item
        }
      })        
    )
  }

  //helper method for reading next user item
  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  //Logout, removes user information from storage
  logout() { 
    localStorage.removeItem('user'); //remove item from local storage with keyword user
    this.currentUserSource.next(null); //Set observable user item as null
  }
}