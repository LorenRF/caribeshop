import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CartService } from '../../services/cart';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.html',
  styleUrls: ['./navbar.css']
})
export class NavbarComponent {
  // Observable que emite la cantidad de ítems en el carrito
  cartCount$: Observable<number>;

  // Nombre del usuario almacenado en localStorage
  userName: string | null = null;

  constructor(private cartService: CartService, private router: Router) {
    // Suscripción al contador de ítems del carrito
    this.cartCount$ = this.cartService.itemsCount$;

    // Carga el nombre de usuario almacenado localmente
    this.userName = localStorage.getItem('username');
  }

  // Método para cerrar sesión, elimina token y usuario del localStorage y redirige a login
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('username');
    this.router.navigate(['/login']);
  }
}
