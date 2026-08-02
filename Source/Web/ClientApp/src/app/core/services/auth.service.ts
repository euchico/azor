import { isPlatformBrowser } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { inject, Injectable, PLATFORM_ID } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AuthResponse, AuthenticatedUser, Credentials } from '../models/auth.models';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private static readonly tokenKey = 'azor.access-token';
  private readonly http = inject(HttpClient);
  private readonly platformId = inject(PLATFORM_ID);
  private readonly apiUrl = `${environment.apiBaseUrl}/auth`;

  register(credentials: Credentials): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/register`, credentials);
  }

  login(credentials: Credentials): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, credentials).pipe(
      tap(response => this.storeToken(response.accessToken))
    );
  }

  getCurrentUser(): Observable<AuthenticatedUser> {
    return this.http.get<AuthenticatedUser>(`${this.apiUrl}/me`);
  }

  getToken(): string | null {
    return this.isBrowser ? localStorage.getItem(AuthService.tokenKey) : null;
  }

  isAuthenticated(): boolean {
    return this.getToken() !== null;
  }

  logout(): void {
    if (this.isBrowser) {
      localStorage.removeItem(AuthService.tokenKey);
    }
  }

  private get isBrowser(): boolean {
    return isPlatformBrowser(this.platformId);
  }

  private storeToken(token: string): void {
    if (this.isBrowser) {
      localStorage.setItem(AuthService.tokenKey, token);
    }
  }
}
