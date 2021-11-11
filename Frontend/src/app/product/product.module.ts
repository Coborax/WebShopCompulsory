import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductRoutingModule } from './product-routing.module';
import { ProductListComponent } from './product-list/product-list.component';
import {LoadingComponent} from "../shared/loading/loading.component";
import {NgbAlertModule} from "@ng-bootstrap/ng-bootstrap";


@NgModule({
  declarations: [
    ProductListComponent,
    LoadingComponent
  ],
  imports: [
    CommonModule,
    ProductRoutingModule,
    NgbAlertModule
  ]
})
export class ProductModule { }
