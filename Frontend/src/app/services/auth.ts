// src/app/services/auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' }) // Servicio disponible globalmente
export class AuthService {
  // URL para registro de usuario
  private registerUrl = 'https://localhost:7185/register';
  // URL para login de usuario
  private loginUrl = 'https://localhost:7185/login';

  constructor(private http: HttpClient) {}

  // Envía datos para registrar un nuevo usuario
  register(data: {
    userName: string;
    name: string;
    lastName: string;
    email: string;
    password: string;
    address: string;
  }): Observable<any> {
    return this.http.post<any>(this.registerUrl, data);
  }

  // Envía credenciales para autenticación y recibe token JWT como texto
  login(username: string, password: string): Observable<string> {
    const params = new HttpParams()
      .set('username', username)
      .set('password', password);

    return this.http.post(this.loginUrl, null, {
      params,
      responseType: 'text' // La respuesta es un token en texto plano
    });
  }
}
