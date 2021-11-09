import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TodosRoutingModule } from './todos-routing.module';
import { TodosOverviewComponent } from './todos-overview/todos-overview.component';
import {FormsModule} from '@angular/forms';


@NgModule({
  declarations: [
    TodosOverviewComponent
  ],
  imports: [
    CommonModule,
    TodosRoutingModule,
    FormsModule
  ]
})
export class TodosModule { }
