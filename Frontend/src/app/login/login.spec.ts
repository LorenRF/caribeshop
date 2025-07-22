import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginComponent } from './login';

describe('Login', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoginComponent]  // Importa el componente Login para la prueba
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();  // Ejecuta detección de cambios inicial
  });

  // Verifica que el componente Login se cree correctamente
  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
