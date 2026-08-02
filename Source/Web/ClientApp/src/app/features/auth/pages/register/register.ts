import { HttpErrorResponse } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register {
  private readonly authService = inject(AuthService);

  readonly errorMessage = signal('');
  readonly successMessage = signal('');
  readonly form = new FormGroup({
    email: new FormControl('', { nonNullable: true, validators: [Validators.required, Validators.email] }),
    password: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required, Validators.minLength(8), Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).*$/)]
    })
  });

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.errorMessage.set('');
    this.successMessage.set('');
    this.authService.register(this.form.getRawValue()).subscribe({
      next: () => {
        this.form.reset();
        this.successMessage.set('Cadastro realizado. Agora você já pode entrar.');
      },
      error: (error: HttpErrorResponse) => {
        const errors = error.error?.errors as string[] | undefined;
        this.errorMessage.set(errors?.join(' ') ?? 'Não foi possível criar a conta.');
      }
    });
  }
}
