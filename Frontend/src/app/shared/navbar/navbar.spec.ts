import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NavbarComponent } from './navbar';
import { Router } from '@angular/router';
import { CartService } from '../../services/cart';
import { of } from 'rxjs';

describe('NavbarComponent', () => {
  let component: NavbarComponent;
  let fixture: ComponentFixture<NavbarComponent>;
  let routerMock: any;
  let cartServiceMock: any;

  beforeEach(async () => {
    // Mock del token JWT simulado
    const fakeToken = generateFakeToken({
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name': 'Carlos López'
    });

    localStorage.setItem('token', fakeToken);

    cartServiceMock = {
      itemsCount$: of(2)
    };

    routerMock = {
      navigate: jasmine.createSpy('navigate')
    };

    await TestBed.configureTestingModule({
      imports: [NavbarComponent],
      providers: [
        { provide: CartService, useValue: cartServiceMock },
        { provide: Router, useValue: routerMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(NavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  afterEach(() => {
    localStorage.clear();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should decode username from token', () => {
    expect(component.userName).toBe('Carlos López');
  });

  it('should clear token and navigate to login on logout', () => {
    component.logout();
    expect(localStorage.getItem('token')).toBeNull();
    expect(routerMock.navigate).toHaveBeenCalledWith(['/login']);
  });

  function generateFakeToken(payload: any): string {
    const base64Payload = btoa(JSON.stringify(payload));
    return `header.${base64Payload}.signature`;
  }
});
