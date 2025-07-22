import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ProductService, Product } from '../../services/product';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './product-list.html',
  styleUrls: ['./product-list.css']
})
export class ProductListComponent implements OnInit {
  // Array que almacena la lista de productos
  products: Product[] = [];

  constructor(private productService: ProductService) {}

  // Se ejecuta al iniciar el componente
  ngOnInit(): void {
    // Suscripción para obtener productos desde el servicio
    this.productService.getProducts().subscribe({
      next: data => this.products = data, // Asigna datos recibidos
      error: err => console.error('Error fetching products', err) // Manejo de error
    });
  }
}
