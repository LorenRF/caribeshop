import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CartComponent } from './cart';

describe('Cart', () => {
  let component: CartComponent;
  let fixture: ComponentFixture<CartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CartComponent]  // Importa el componente Cart para las pruebas
    })
    .compileComponents();

    fixture = TestBed.createComponent(CartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges(); // Detecta cambios y renderiza el componente
  });

  // Verifica que el componente se haya creado correctamente
  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
