import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CheckoutComponent } from './checkout';
import { CartService } from '../../services/cart';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

describe('CheckoutComponent', () => {
  let component: CheckoutComponent;
  let fixture: ComponentFixture<CheckoutComponent>;
  let cartServiceMock: any;
  let routerMock: any;

  // Configuración antes de cada prueba
  beforeEach(async () => {
    // Mock para CartService
    cartServiceMock = {
      getTotal: jasmine.createSpy().and.returnValue(150), // Simula getTotal retornando 150
      clearCart: jasmine.createSpy() // Simula método clearCart
    };

    // Mock para Router
    routerMock = {
      navigate: jasmine.createSpy('navigate') // Simula método navigate
    };

    await TestBed.configureTestingModule({
      imports: [CheckoutComponent, FormsModule], // Importa componente y FormsModule
      providers: [
        { provide: CartService, useValue: cartServiceMock }, // Inyecta mock de CartService
        { provide: Router, useValue: routerMock } // Inyecta mock de Router
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(CheckoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges(); // Detecta cambios iniciales
  });

  // Verifica que el componente se crea correctamente
  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  // Verifica que getTotal llama al servicio y retorna valor correcto
  it('should return the total from CartService', () => {
    expect(component.getTotal()).toBe(150);
    expect(cartServiceMock.getTotal).toHaveBeenCalled();
  });

  // No confirma el pedido si la dirección está vacía o solo con espacios
  it('should not confirm order if address is empty', () => {
    spyOn(window, 'alert'); // Espía alert para verificar llamada
    component.manualAddress = '   '; // Dirección vacía con espacios
    component.confirmOrder();
    expect(cartServiceMock.clearCart).not.toHaveBeenCalled();
    expect(routerMock.navigate).not.toHaveBeenCalled();
    expect(window.alert).toHaveBeenCalledWith('❗ Por favor, ingresa tu dirección.');
  });

  // Confirma el pedido si la dirección es válida
  it('should confirm order when address is valid', () => {
    spyOn(window, 'alert');
    component.manualAddress = 'Av. Duarte 123';
    component.confirmOrder();
    expect(cartServiceMock.clearCart).toHaveBeenCalled();
    expect(routerMock.navigate).toHaveBeenCalledWith(['/products']);
    expect(window.alert).toHaveBeenCalledWith('✅ Pedido confirmado. ¡Gracias por tu compra!');
  });
});
