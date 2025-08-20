import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div style="display:flex; gap:12px; align-items:center; margin-bottom: 1rem;">
      <button *ngIf="!(auth.isAuthenticated$ | async)" (click)="auth.loginWithRedirect()">
        Login
      </button>
  <button *ngIf="auth.isAuthenticated$ | async" (click)="auth.logout({ logoutParams: { returnTo: returnTo } })">
        Logout
      </button>
    </div>

    <div *ngIf="auth.user$ | async as user; else guest">
      <p>Welcome, {{ user.name || user.email }}.</p>
    </div>
    <ng-template #guest>
      <p>Please login to continue.</p>
    </ng-template>
  `
})
export class HomeComponent {
  public readonly returnTo = typeof window !== 'undefined' ? window.location.origin : '/';
  constructor(public auth: AuthService) {}
}
