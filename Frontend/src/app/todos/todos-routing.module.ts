import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {TodosOverviewComponent} from './todos-overview/todos-overview.component';

//todos
const routes: Routes = [
  {
    path: '', component: TodosOverviewComponent
  }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TodosRoutingModule { }
