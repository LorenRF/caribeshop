import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ProductService, Product } from '../../services/product';
import { RouterModule } from '@angular/router';
import { CartService } from '../../services/cart';


@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './product-detail.html',
  styleUrls: ['./product-detail.css']
})
export class ProductDetailComponent implements OnInit {
  product: Product | undefined;

  constructor(
  private route: ActivatedRoute,
  private productService: ProductService,
  private cartService: CartService
) {}

addToCart() {
  if (this.product) {
    this.cartService.addToCart(this.product);
    alert('Product added to cart!');
  }
}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id) {
      this.productService.getProductById(id).subscribe({
        next: data => this.product = data,
        error: err => console.error('Error loading product:', err)
      });
    }
  }
}
