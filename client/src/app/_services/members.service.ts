import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) { }

  //Get users with photos and age auto mapped
  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'users');
  }

  //Get one user by username (with photos and age auto mapped)
  getMember(username: string) {
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }
}
