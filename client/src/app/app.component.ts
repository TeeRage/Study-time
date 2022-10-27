import { Component } from '@angular/core';

//Decorator that gives normal classess some more extra powers (our class AppComponent is now an angular component)
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
//Class with title property
export class AppComponent {
  title = 'My awesome app';
}