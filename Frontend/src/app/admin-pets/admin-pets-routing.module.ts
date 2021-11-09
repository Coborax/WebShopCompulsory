import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {PetsOverviewComponent} from './pets-overview/pets-overview.component';

const routes: Routes = [{
  path: '', component: PetsOverviewComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminPetsRoutingModule { }
