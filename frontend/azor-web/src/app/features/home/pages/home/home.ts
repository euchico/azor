import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ApiHealthService } from '../../../../core/services/api-health.service';

@Component({
  selector: 'app-home',
  imports: [RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.scss'
})

export class Home{
  private readonly apiHealthService = inject(ApiHealthService);

  apiStatus = signal('Conexão ainda não testada.');

  testApiConnection(): void {
    this.apiStatus.set('Testando conexão com a API...');

    this.apiHealthService.getHealth().subscribe({
      next: (response) => {
        this.apiStatus.set(`${response.message}`);
      },
      error: () => {
        this.apiStatus.set('Não foi possível conectar com a API.');
      }
    });
  }
}