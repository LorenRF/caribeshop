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
  // Variable para almacenar el producto a mostrar
  product: Product | undefined;

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private cartService: CartService
  ) {}

  // Método para agregar el producto actual al carrito
  addToCart() {
    if (this.product) {
      this.cartService.addToCart(this.product);
      alert('Product added to cart!');
    }
  }

  // Se ejecuta al inicializar el componente
  ngOnInit(): void {
    // Obtiene el id del producto desde la URL
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id) {
      // Solicita el producto por id desde el servicio
      this.productService.getProductById(id).subscribe({
        next: data => this.product = data,  // Asigna producto recibido
        error: err => console.error('Error loading product:', err)  // Manejo de error
      });
    }
  }
}
