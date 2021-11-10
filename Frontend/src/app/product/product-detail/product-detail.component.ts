import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {ProductDto} from "../shared/product.dto";
import {ProductService} from "../shared/product.service";
import {Location} from "@angular/common";

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {

  product: ProductDto | undefined;

  constructor(private route: ActivatedRoute, private service: ProductService, private location: Location) {

  }
  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.service.getProductById(id).subscribe(p => this.product = p);
  }

  goBack(): void {
    this.location.back();
  }

}
