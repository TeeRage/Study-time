import { NgModule } from '@angular/core';
//This is for tidying up the app.module, we import needed extra things and export them in one place
//This is a good thing to do, so that app.module wont get too big
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(), //NGx bootstrap dropdown
    ToastrModule.forRoot({ //Toaster messages
      positionClass: 'toast-bottom-right'
    })
  ],
  exports: [
    BsDropdownModule,
    ToastrModule
  ]
})

export class SharedModule { }
