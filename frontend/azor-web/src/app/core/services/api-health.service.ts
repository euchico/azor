import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

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

  private readonly apiUrl = 'http://localhost:5041/api/health';

  getHealth(): Observable<ApiHealthResponse> {
    return this.http.get<ApiHealthResponse>(this.apiUrl);
  }
}