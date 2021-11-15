import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {ProductDto} from "./product.dto";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  createProducts(productCreate: ProductDto): Observable<ProductDto> {
    const headers = { 'content-type': 'application/json'}
    const body = JSON.stringify(productCreate)

    return this.http.post<ProductDto>("https://localhost:5001/api/Product", body, {'headers':headers});
  }

  getProducts(): Observable<ProductDto[]> {
    return this.http.get<ProductDto[]>("https://localhost:5001/api/Product");
  }

  deleteProduct(id: number): Observable<ProductDto> {
    return this.http.delete<ProductDto>("https://localhost:5001/api/Product?id=" + id);
  }

  getProductById(id: number): Observable<ProductDto> {
    return this.http.get<ProductDto>("https://localhost:5001/api/Product/" + id);
  }
}
