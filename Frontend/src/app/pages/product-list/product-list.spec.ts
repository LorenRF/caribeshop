import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProductListComponent } from './product-list';

// Suite de pruebas para ProductListComponent
describe('ProductList', () => {
  let component: ProductListComponent;
  let fixture: ComponentFixture<ProductListComponent>;

  // Configuración antes de cada prueba
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductListComponent] // Importa el componente standalone
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductListComponent); // Crea instancia
    component = fixture.componentInstance;
    fixture.detectChanges(); // Detecta cambios iniciales
  });

  // Prueba básica para verificar que el componente se crea correctamente
  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
