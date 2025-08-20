import { Routes } from '@angular/router';
import { AuthGuard } from '@auth0/auth0-angular';
import { HomeComponent } from './home.component';
import { ProtectedComponent } from './protected.component';

export const routes: Routes = [
	{ path: '', component: HomeComponent },
	{ path: 'protected', component: ProtectedComponent, canActivate: [AuthGuard] },
	{ path: '**', redirectTo: '' }
];
