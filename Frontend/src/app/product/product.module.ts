import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductRoutingModule } from './product-routing.module';
import { ProductListComponent } from './product-list/product-list.component';
import {LoadingComponent} from "../shared/loading/loading.component";
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { UpdateProductComponent } from './update-product/update-product.component';
import {FormsModule} from "@angular/forms";


@NgModule({
  declarations: [
    ProductListComponent,
    LoadingComponent,
    ProductDetailComponent,
    UpdateProductComponent
  ],
    imports: [
        CommonModule,
        ProductRoutingModule,
        FormsModule
    ]
})
export class ProductModule { }
