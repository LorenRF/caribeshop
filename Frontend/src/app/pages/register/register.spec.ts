import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterComponent } from './register';

// Suite de pruebas para el componente Register
describe('Register', () => {
  let component: RegisterComponent;
  let fixture: ComponentFixture<RegisterComponent>;

  // Configuración antes de cada prueba
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterComponent] // Importa el componente standalone
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterComponent); // Crea instancia del componente
    component = fixture.componentInstance;
    fixture.detectChanges(); // Detecta cambios iniciales
  });

  // Prueba básica para verificar que el componente se crea correctamente
  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
