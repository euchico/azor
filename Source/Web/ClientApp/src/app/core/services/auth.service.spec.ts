import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { AuthService } from './auth.service';

describe('AuthService', () => {
  let service: AuthService;
  let http: HttpTestingController;

  beforeEach(() => {
    localStorage.clear();
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()]
    });
    service = TestBed.inject(AuthService);
    http = TestBed.inject(HttpTestingController);
  });

  afterEach(() => http.verify());

  it('stores the JWT after a successful login', () => {
    service.login({ email: 'user@example.com', password: 'Password1' }).subscribe();

    const request = http.expectOne('http://localhost:5041/api/auth/login');
    request.flush({
      accessToken: 'jwt-token',
      expiresAt: '2026-08-01T00:00:00Z',
      user: { id: '1', email: 'user@example.com' }
    });

    expect(service.getToken()).toBe('jwt-token');
    expect(service.isAuthenticated()).toBe(true);
  });

  it('removes the JWT on logout', () => {
    localStorage.setItem('azor.access-token', 'jwt-token');

    service.logout();

    expect(service.isAuthenticated()).toBe(false);
  });
});
