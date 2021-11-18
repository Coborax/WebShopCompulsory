import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {ProductService} from "../shared/product.service";
import {Location} from "@angular/common";
import {ProductDto} from "../shared/product.dto";
import {PutProductDto} from "../shared/put-product-dto";

@Component({
  selector: 'app-update-product',
  templateUrl: './update-product.component.html',
  styleUrls: ['./update-product.component.scss']
})
export class UpdateProductComponent implements OnInit {

  product: ProductDto | undefined;
  nameUp: string|undefined
  descriptionUp: string|undefined
  private response: ProductDto|undefined;

  constructor(private route: ActivatedRoute, private service: ProductService, private location: Location) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.service.getProductById(id).subscribe((p) =>{this.product = p
      this.nameUp = this.product.name
      this.descriptionUp = this.product.desc
    } );

  }

  updateProduct(): void{

      // @ts-ignore
    let updadedProduct = {Id: this.product.id, Name: this.nameUp, Desc: this.descriptionUp} as PutProductDto;

    this.service.updateProduct(updadedProduct.Id,updadedProduct).subscribe((p) =>{this.response = p
      console.log(this.response.name + "success or what")
    } );
  }

}
