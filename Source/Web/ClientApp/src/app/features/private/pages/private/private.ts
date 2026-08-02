import { Component, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-private',
  imports: [RouterLink],
  templateUrl: './private.html',
  styleUrl: './private.scss'
})
export class Private {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  readonly email = signal('');

  constructor() {
    this.authService.getCurrentUser().subscribe({
      next: user => this.email.set(user.email)
    });
  }

  logout(): void {
    this.authService.logout();
    void this.router.navigate(['/login']);
  }
}
