import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface ApiHealthResponse {
  status: string;
  app: string;
  message: string;
  timestamp: string;
}

@Injectable({
  providedIn: 'root'
})
export class ApiHealthService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl = `${environment.apiBaseUrl}/health`;

  getHealth(): Observable<ApiHealthResponse> {
    return this.http.get<ApiHealthResponse>(this.apiUrl);
  }
}
