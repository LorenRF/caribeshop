import { Injectable } from '@angular/core';
import { Product } from './product';
import { BehaviorSubject } from 'rxjs';

// Interfaz que representa un ítem en el carrito
export interface CartItem {
  product: Product;
  quantity: number;
}

@Injectable({
  providedIn: 'root' // Servicio disponible globalmente
})
export class CartService {
  // Array que almacena los ítems del carrito
  private items: CartItem[] = [];

  // BehaviorSubject para emitir el conteo total de ítems
  private itemsCount = new BehaviorSubject<number>(0);

  // Observable público para que otros componentes puedan suscribirse al conteo
  itemsCount$ = this.itemsCount.asObservable();

  // Actualiza el conteo total y emite el nuevo valor
  private updateCount() {
    const count = this.items.reduce((acc, item) => acc + item.quantity, 0);
    this.itemsCount.next(count);
  }

  // Retorna los ítems actuales en el carrito
  getItems(): CartItem[] {
    return this.items;
  }

  // Añade un producto al carrito, incrementando cantidad si ya existe
  addToCart(product: Product): void {
    const existingItem = this.items.find(item => item.product.id === product.id);
    if (existingItem) {
      existingItem.quantity++;
    } else {
      this.items.push({ product, quantity: 1 });
    }
    this.updateCount();
  }

  // Disminuye la cantidad de un producto, elimina si llega a cero
  decreaseItem(productId: number): void {
    const item = this.items.find(i => i.product.id === productId);
    if (item) {
      item.quantity--;
      if (item.quantity === 0) {
        this.removeItem(productId);
      }
    }
    this.updateCount();
  }

  // Elimina un producto completamente del carrito
  removeItem(productId: number): void {
    this.items = this.items.filter(item => item.product.id !== productId);
    this.updateCount();
  }

  // Limpia todos los ítems del carrito
  clearCart(): void {
    this.items = [];
    this.updateCount();
  }

  // Calcula el total acumulado del carrito
  getTotal(): number {
    return this.items.reduce((total, item) => total + item.product.price * item.quantity, 0);
  }
}
