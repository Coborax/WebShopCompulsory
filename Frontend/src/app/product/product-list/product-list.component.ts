import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {ProductService} from "../shared/product.service";
import {ProductDto} from "../shared/product.dto";
import {UserDto} from "../../auth/shared/user.dto";
import {AuthService} from "../../auth/shared/auth.service";

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {

  products: ProductDto[] | undefined;
  loadingProducts: boolean = true;
  user: UserDto | undefined;

  constructor(private productService: ProductService, private authServie: AuthService) { }

  ngOnInit(): void {
    this.user = this.authServie.getUserObject()?.user;

    this.productService.getProducts()
      .subscribe(products => {
        this.products = products;
        this.loadingProducts = false;
    });
  }

  delete(product: ProductDto) {
    this.productService.deleteProduct(product.id)
      .subscribe(() => this.ngOnInit());
  }
}
