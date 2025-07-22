import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './register.html'
})
export class RegisterComponent {
  // Modelo para los datos del formulario de registro
  form = {
    userName: '',
    name: '',
    lastName: '',
    email: '',
    password: '',
    address: ''
  };

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  // Método para enviar el formulario de registro
  register() {
    this.authService.register(this.form).subscribe({
      next: (res: any) => {
        // Muestra alerta de éxito y redirige a login
        alert('✅ ' + (typeof res === 'string' ? res : 'Registro exitoso'));
        this.router.navigate(['/login']);
      },
      error: (err) => {
        // Manejo de error mostrando mensaje adecuado
        const msg = typeof err.error === 'string' ? err.error : 'Algo salió mal.';
        
        if (msg.includes('created') || msg.includes('registrado')) {
          alert('✅ ' + msg);
          this.router.navigate(['/login']);
        } else {
          alert('❌ Error al registrarse: ' + msg);
        }
      }
    });
  }
}
