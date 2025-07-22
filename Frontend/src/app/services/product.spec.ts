import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// Interfaz que define la estructura de un producto
export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  imageUrl: string;
}

@Injectable({
  providedIn: 'root' // Servicio inyectable en toda la app
})
export class ProductService {
  // URL base del API para productos
  private baseUrl = 'https://localhost:7185/api/Products'; 

  constructor(private http: HttpClient) {}

  // Obtiene la lista completa de productos desde la API
  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl);
  }

  // Obtiene un producto específico por su id
  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/${id}`);
  }
}
