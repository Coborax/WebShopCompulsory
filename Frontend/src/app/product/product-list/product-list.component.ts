import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {ProductService} from "../shared/product.service";
import {ProductDto} from "../shared/product.dto";

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {

  products: ProductDto[] | undefined;
  loadingProducts: boolean = true;

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.productService.getProducts()
      .subscribe(products => {
        this.products = products;
        this.loadingProducts = false;
    })
  }

  delete(product: ProductDto) {
    this.productService.deleteProduct(product.id)
      .subscribe(() => this.ngOnInit())
  }
}
