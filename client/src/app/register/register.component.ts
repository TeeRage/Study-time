import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  //To send data from child to parent (home), we use eventEmitter. Used in home component.html.
  @Output() cancelRegister = new EventEmitter();

  model: any = {};

  //Inject accountService to this component
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  register() {
    this.accountService.register(this.model).subscribe(response => {
      console.log(response);
      this.cancel(); //close register form after registeration.
    }, error => {
      console.log(error);
    })
  }

  cancel() {
    this.cancelRegister.emit(false); //Cancel button emits false
  }
}
