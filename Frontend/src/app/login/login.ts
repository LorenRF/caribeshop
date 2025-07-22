import { Component } from '@angular/core';
import { AuthService } from '../services/auth';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.html'
})
export class LoginComponent {
  // Variables para almacenar el usuario y contraseña ingresados
  username: string = '';
  password: string = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  // Método que gestiona el proceso de login
  login(): void {
    // Validación básica de campos vacíos
    if (!this.username.trim() || !this.password.trim()) {
      alert('⚠️ Por favor, ingresa usuario y contraseña.');
      return;
    }

    // Llama al servicio de autenticación para login
    this.authService.login(this.username, this.password).subscribe({
      next: (res: string) => {
        // Verifica si la respuesta es un token válido (string con más de 20 caracteres)
        if (res && typeof res === 'string' && res.length > 20) {
          // Guarda token y username en localStorage
          localStorage.setItem('token', res);
          localStorage.setItem('username', this.username);
          // Navega a la página de productos
          this.router.navigate(['/products']);
        } else {
          alert('❌ Credenciales inválidas. Intenta de nuevo.');
        }
      },
      // Muestra alerta si ocurre un error en la petición
      error: (err) => {
        alert('❌ Error al iniciar sesión: ' + err.message);
      }
    });
  }

  // Navega a la página de registro
  goToRegister() {
    this.router.navigate(['/register']);
  }
}
