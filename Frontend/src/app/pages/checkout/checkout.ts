import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartService } from '../../services/cart';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './checkout.html'
})
export class CheckoutComponent {
  // Variable para almacenar la dirección ingresada manualmente
  manualAddress: string = '';

  constructor(private cartService: CartService, private router: Router) {}

  // Obtiene el total del carrito usando el servicio
  getTotal(): number {
    return this.cartService.getTotal();
  }

  // Confirma el pedido, valida la dirección y limpia el carrito
  confirmOrder(): void {
    if (!this.manualAddress.trim()) {
      alert('❗ Por favor, ingresa tu dirección.');
      return;
    }

    // Aquí se podría enviar la dirección al backend (no implementado)
    console.log('Dirección ingresada:', this.manualAddress);

    // Limpia el carrito
    this.cartService.clearCart();

    alert('✅ Pedido confirmado. ¡Gracias por tu compra!');
    // Redirige a la lista de productos
    this.router.navigate(['/products']);
  }
}
