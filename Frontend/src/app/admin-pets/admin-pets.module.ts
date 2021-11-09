import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminPetsRoutingModule } from './admin-pets-routing.module';
import { PetsOverviewComponent } from './pets-overview/pets-overview.component';


@NgModule({
  declarations: [
    PetsOverviewComponent
  ],
  imports: [
    CommonModule,
    AdminPetsRoutingModule
  ]
})
export class AdminPetsModule { }
