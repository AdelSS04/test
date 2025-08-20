import { Component } from '@angular/core';

@Component({
  selector: 'app-protected',
  standalone: true,
  template: `
    <h2>Protected area</h2>
    <p>If you can see this, you're authenticated.</p>
  `
})
export class ProtectedComponent {}
