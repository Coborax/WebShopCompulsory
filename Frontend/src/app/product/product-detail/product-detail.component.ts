import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {ProductDto} from "../shared/product.dto";
import {ProductService} from "../shared/product.service";

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {

  product: ProductDto | undefined;

  constructor(private route: ActivatedRoute, private service: ProductService ) {

  }
  ngOnInit(): void {
   this.route.queryParams.subscribe(params => {
     this.service.getProductById(params['id']).subscribe(p => this.product=p)
   })

  }

}
