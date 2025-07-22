import { TestBed } from '@angular/core/testing';
import { AuthService } from './auth';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

// Suite de pruebas para AuthService
describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;

  // Datos simulados para registro
  const dummyRegisterData = {
    userName: 'testuser',
    name: 'Test',
    lastName: 'User',
    email: 'test@example.com',
    password: '123456',
    address: 'Av. Duarte 123'
  };

  // Configuración antes de cada prueba
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule], // módulo para pruebas HTTP
      providers: [AuthService]
    });

    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  // Verifica que no haya peticiones HTTP pendientes luego de cada prueba
  afterEach(() => {
    httpMock.verify();
  });

  // Prueba para verificar creación del servicio
  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  // Prueba que verifica que se envíe POST a /register con los datos correctos
  it('should send POST request to /register with form data', () => {
    service.register(dummyRegisterData).subscribe();

    const req = httpMock.expectOne('https://localhost:7185/register');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(dummyRegisterData);

    req.flush({}); // Simula respuesta exitosa del servidor
  });

  // Prueba que verifica que se envíe POST a /login con query params y body nulo
  it('should send POST request to /login with query params', () => {
    const username = 'admin';
    const password = 'admin';

    service.login(username, password).subscribe();

    const req = httpMock.expectOne(
      `https://localhost:7185/login?username=${username}&password=${password}`
    );

    expect(req.request.method).toBe('POST');
    expect(req.request.body).toBeNull(); // El body es null porque se usan params

    req.flush({ token: 'fake-jwt-token' }); // Simula respuesta con token
  });
});
