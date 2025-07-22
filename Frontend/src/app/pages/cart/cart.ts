import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartService, CartItem } from '../../services/cart';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './cart.html',
  styleUrls: ['./cart.css']
})
export class CartComponent implements OnInit {
  // Arreglo para almacenar los ítems del carrito
  cartItems: CartItem[] = [];

  constructor(private cartService: CartService) {}

  // Inicializa la lista de ítems desde el servicio al cargar el componente
  ngOnInit(): void {
    this.cartItems = this.cartService.getItems();
  }

  // Incrementa la cantidad del ítem en el carrito
  increase(item: CartItem): void {
    this.cartService.addToCart(item.product);
  }

  // Disminuye la cantidad del ítem en el carrito
  decrease(item: CartItem): void {
    this.cartService.decreaseItem(item.product.id);
  }

  // Remueve completamente el ítem del carrito y actualiza la lista local
  remove(item: CartItem): void {
    this.cartService.removeItem(item.product.id);
    this.cartItems = this.cartService.getItems(); // Actualiza la lista
  }

  // Limpia todos los ítems del carrito y la lista local
  clearCart(): void {
    this.cartService.clearCart();
    this.cartItems = [];
  }

  // Obtiene el total del carrito desde el servicio
  getTotal(): number {
    return this.cartService.getTotal();
  }
}
