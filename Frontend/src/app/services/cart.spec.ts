import { TestBed } from '@angular/core/testing';
import { CartService } from './cart';

// Suite de pruebas para el servicio Cart
describe('Cart', () => {
  let service: CartService;

  // Se ejecuta antes de cada prueba para configurar el entorno y crear instancia del servicio
  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CartService);
  });

  // Prueba básica para verificar que el servicio Cart se crea correctamente
  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
