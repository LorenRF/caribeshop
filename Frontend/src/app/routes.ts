import { Routes } from '@angular/router';
import { ProductListComponent } from './pages/product-list/product-list';
import { ProductDetailComponent } from './pages/product-detail/product-detail';
import { CartComponent } from './pages/cart/cart';
import { CheckoutComponent } from './pages/checkout/checkout';
import { RegisterComponent } from './pages/register/register';
import { LoginComponent } from './login/login';
import { authGuard } from './guards/auth.guard';

// Definición de rutas principales de la aplicación Angular
export const routes: Routes = [
  // Redirige la ruta raíz ('') a la página de login
  { path: '', redirectTo: 'login', pathMatch: 'full' },

  // Ruta pública para login
  { path: 'login', component: LoginComponent },
  // Ruta pública para registro de usuario
  { path: 'register', component: RegisterComponent },

  // Ruta protegida que muestra la lista de productos
  { path: 'products', component: ProductListComponent, canActivate: [authGuard] },
  // Ruta protegida para ver detalle de un producto según su id
  { path: 'product/:id', component: ProductDetailComponent, canActivate: [authGuard] },
  // Ruta protegida para ver el carrito de compras
  { path: 'cart', component: CartComponent, canActivate: [authGuard] },
  // Ruta protegida para la página de checkout
  { path: 'checkout', component: CheckoutComponent, canActivate: [authGuard] },

  // Ruta comodín para redirigir cualquier URL no definida al login
  { path: '**', redirectTo: 'login' }
];
